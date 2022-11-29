/*
Author: Robert Davidson
Partner: David Clark
Date: 11/27/2022
Course: CS 4540, University of Utah, School of Computing
Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.

I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from
another source.  Any references used in the completion of the assignment are cited in my README file.

I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from
another source. Any references used in the completion of the assignment are cited in my README file.

File Contents

Draws availability schedule for user. Allows editing and saving availability.
*/




class Slot extends PIXI.Graphics {
	static selected_valid = false;
	static selected_slot_num = -1;
	static selected_availability = true;
	constructor(availability_tracker, slot_num, available, x, y, my_width, my_height, slot_id, slot_tauserid)
	{
		super();

		this.slot_id = slot_id;
		this.slot_tauserid = slot_tauserid;
		this.availability_tracker = availability_tracker;

		this.slot_num = slot_num;
		this.available = available;
		this.x = x;
		this.y = y;
		this.my_width = my_width;
		this.my_height = my_height;

		this.interactive = true;

		this.on('mousedown', this.pointer_down);
		this.on('mouseup', this.pointer_up);
		this.on('mouseover', this.pointer_over);
	}

	draw_slot()
	{
		this.clear();
		var color = ((this.available) ? 0x3c42f0 : 0xe2e4fa);
		this.beginFill(color);
		this.drawRect(0, 0, this.my_width, this.my_height);
	}

	pointer_down()
	{
		Slot.selected_valid = true;
		Slot.selected_slot_num = this.slot_num;
		Slot.selected_availability = this.available;

		this.available = !this.available;
		this.draw_slot();
		this.availability_tracker.mouse_down = true;
	}

	pointer_up() {
		Slot.selected_valid = false;
		this.availability_tracker.mouse_down = false;

		var selected_col = Math.floor(Slot.selected_slot_num/48);
		var this_col = Math.floor(this.slot_num/48);
		if(selected_col != this_col){
			return;
		}

		if(Slot.selected_slot_num < this.slot_num){
			for(var i = Slot.selected_slot_num; i <= this.slot_num; i++){
				this.availability_tracker.slots[i].available = !Slot.selected_availability;
				this.availability_tracker.slots[i].draw_slot();
			}
		} else {
			for(var i = Slot.selected_slot_num; i >= this.slot_num; i--){
				this.availability_tracker.slots[i].available = !Slot.selected_availability;
				this.availability_tracker.slots[i].draw_slot();
			}
		}
	}

	pointer_over() {
		var selected_col = Math.floor(Slot.selected_slot_num/48);
		var this_col = Math.floor(this.slot_num/48);
		if(selected_col != this_col){
			return;
		}

		if(this.availability_tracker.mouse_down){
			this.available = !Slot.selected_availability;
			this.draw_slot();
		}
	}
}


class AvailabilityTracker extends PIXI.Graphics {

	static bg_color = 0xe2e4fa;
	static border_color = 0xa9a9a9;
	static available_color = 0x343464;
	static unavailable_color = 0x2f2f2f;
	static width = 800;
	static height = 600;



	constructor(){
		super();

		this.mouse_down = false;
		this.on('mouseup', this.pointer_up);

		this.app = new PIXI.Application({ backgroundColor: AvailabilityTracker.bg_color });
		this.app.renderer.resize(AvailabilityTracker.width, AvailabilityTracker.height);
		$("#canvas_div").append(this.app.view);

		this.slots = this.build_slots();
		this.grid = this.build_grid();

		this.load_slots();
	}


	build_grid() {
		const numDaysWithBuffer = 6;
		const numSlotsWithBuffer = 56;
		const slotWidth = AvailabilityTracker.width / numDaysWithBuffer;
		const slotHeight = AvailabilityTracker.height / numSlotsWithBuffer;
		var grid = new PIXI.Graphics();
		grid.x = 20;
		grid.y = 4 * slotHeight;
		grid.beginFill(AvailabilityTracker.border_color);
        // Horizontal Lines
		for (var i = 0; i < numSlotsWithBuffer - 6; i += 4) {
			grid.drawRect(0, i * slotHeight, AvailabilityTracker.width - slotWidth, 1);
		}
		this.app.stage.addChild(grid);
		return grid;
	}

	build_slots() {
		const numDaysWithBuffer = 6;
		const numSlotsWithBuffer = 56;
		const slotWidth = AvailabilityTracker.width / numDaysWithBuffer;
		const slotHeight = AvailabilityTracker.height / numSlotsWithBuffer;
		var offsetX = 20;
		var offsetY = 4*slotHeight;

		var xGap = 12;
        let slot_arr = []; // js representation of Slot objects

        // Slot Initialization
        for (let i = 0; i < 5 * 48; i++) {
        	var x = offsetX + (Math.floor(i / 48) * slotWidth);
        	var y = offsetY + (i % 48 * slotHeight);
    //        var isAvailable = slot_data[i]["isOpen"];

        	var new_slot = new Slot(this, i, true, x + xGap/2, y, slotWidth - xGap, slotHeight);
        	new_slot.draw_slot();
        	this.app.stage.addChild(new_slot);
        	slot_arr.push(new_slot);
        }

        return slot_arr;
   }
    // some thoughts:
    // 1) sometimes "this" is not what you think it is, if so use bind
    // 2) start simple and test, use abstraction

   load_slots() {
   	let existing_slots = [];

   	$.ajax({
   		url: "Availability/GetSchedule/",
   		type: "GET"
   	})
   	.done( (function (data) {
   		existing_slots = data;
   		for(var i = 0; i < 5*48; i++){
   			console.log(i,existing_slots[i]);
   			this.slots[i].slot_id = existing_slots[i]["id"];
   			this.slots[i].slot_tauserid = existing_slots[i]["taUserId"];
   			this.slots[i].available = existing_slots[i]["isOpen"];
   			this.slots[i].draw_slot();
   		}
   	}).bind(this) )
   	.fail(function (data) { console.log("Catastrophe! Slots were not recieved from Database."); });

   	return existing_slots;
   }

   upload_slots() {
   	var data = [];

   	for (var i = 0; i < 5*48; i++) {
   		var slot = new Object();
   		slot.ID = this.slots[i].slot_id;
   		slot.IsOpen  = this.slots[i].available;
   		slot.SlotNumber = this.slots[i].slot_num;
   		slot.TAUserId = this.slots[i].slot_tauserid;
   		var jsonString= JSON.stringify(slot);
   		data.push(jsonString);
   		console.log("MAGIC MAGIC MAGIC" + i, jsonString);
   	}
   	$.ajax({
   		url: "Availability/SetSchedule/",
   		type: "POST",
   		data: data
   	})
   	.done(function (data) {
   		console.log("successfully saved, probably");
   	})
   	.fail(function (data) {
   		console.log("so sad, not saved");
   	})
   }

   pointer_up() {
   	this.mouse_down = false;
   }
}

const availability_tracker = new AvailabilityTracker();
document.getElementById("save_schedule_btn").onclick = function() {availability_tracker.upload_slots()};
