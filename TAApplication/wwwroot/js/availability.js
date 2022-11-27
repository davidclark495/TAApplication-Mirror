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
    static selected_slotNum = -1;
    static selected_availability = true;
    constructor(slotNum, available, x, y, my_width, my_height)
    {
        super();

        this.slotNum = slotNum;
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
        Slot.selected_slotNum = this.slotNum;
        Slot.selected_availability = this.available;

        this.available = !this.available;
        this.draw_slot();
        mouse_down = true;
        console.log("available: "+this.available);
    }

    pointer_up() {
        console.log("pointer up");
        Slot.selected_valid = false;
        mouse_down = false;

        var selected_col = Math.floor(Slot.selected_slotNum/48);
        var this_col = Math.floor(this.slotNum/48);
        if(selected_col != this_col){
            return;
        }

        if(Slot.selected_slotNum < this.slotNum){
            for(var i = Slot.selected_slotNum; i <= this.slotNum; i++){
                slots[i].available = !Slot.selected_availability;
                slots[i].draw_slot();
            }
        } else {
            for(var i = Slot.selected_slotNum; i >= this.slotNum; i--){
                slots[i].available = !Slot.selected_availability;
                slots[i].draw_slot();
            }
        }
    }

    pointer_over() {
        if(mouse_down){
            this.available = !Slot.selected_availability;
            this.draw_slot();
        }
    }
}

const bg_color = 0xe2e4fa;
const border_color = 0xa9a9a9;
const available_color = 0x343464;
const unavailable_color = 0x2f2f2f;
const width = 800;
const height = 600;

var mouse_down = false;

app = new PIXI.Application({ backgroundColor: bg_color });
app.renderer.resize(width, height);
$("#canvas_div").append(app.view);

var slots = build_slots();
var grid = build_grid();

load_slots();

function build_grid() {
    const numDaysWithBuffer = 6;
    const numSlotsWithBuffer = 56;
    const slotWidth = width / numDaysWithBuffer;
    const slotHeight = height / numSlotsWithBuffer;
    var grid = new PIXI.Graphics();
    grid.x = 20;
    grid.y = 4 * slotHeight;
    grid.beginFill(border_color);
// Horizontal Lines
    for (var i = 0; i < numSlotsWithBuffer - 6; i += 4) {
        grid.drawRect(0, i * slotHeight, width - slotWidth, 1);
    }
    app.stage.addChild(grid);
    return grid;
}

function build_slots() {
    const numDaysWithBuffer = 6;
    const numSlotsWithBuffer = 56;
    const slotWidth = width / numDaysWithBuffer;
    const slotHeight = height / numSlotsWithBuffer;
    var offsetX = 20;
    var offsetY = 4*slotHeight;

    var xGap = 12;
let slot_arr = []; // js representation of Slot objects

// Slot Initialization
for (let i = 0; i < 5 * 48; i++) {
    var x = offsetX + (Math.floor(i / 48) * slotWidth);
    var y = offsetY + (i % 48 * slotHeight);
//        var isAvailable = slot_data[i]["isOpen"];

    var new_slot = new Slot(i, true, x + xGap/2, y, slotWidth - xGap, slotHeight);
    new_slot.draw_slot();
    app.stage.addChild(new_slot);
    slot_arr.push(new_slot);
}

return slot_arr;
}
// some thoughts:
// 1) sometimes "this" is not what you think it is, if so use bind
// 2) start simple and test, use abstraction

function load_slots() {
    existing_slots = [];

    $.ajax({
        url: "Availability/GetSchedule/",
        type: "GET"
    })
    .done(function (data) {
        existing_slots = data;
        console.log(data);
        for(var i = 0; i < 5*48; i++){
            slots[i].available = existing_slots[i]["isOpen"];
            slots[i].draw_slot();
            console.log("slot data i: " + i + " " + existing_slots[i]["isOpen"]);

        }
    })
    .fail(function (data) { console.log("Catastrophe! Slots were not recieved from Database."); });

    return existing_slots;
}

function upload_slots(){
    var data;
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