let bg_color = 0x000055;
let rect_color = 0xeeee55;
let border_color = 0xaaaaa1;
let width = 800;
let height = 600;

var color = 0xffffff;
var mouse_down = false;

app = new PIXI.Application({ backgroundColor: bg_color });
app.renderer.resize(width, height);
$("#canvas_div").append(app.view);

var square_1 = build_square(1);
var square_2 = build_square(2);
var slots = build_slots();
var grid = build_grid();

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
    // Vertical Lines
    for (var i = 0; i < numDaysWithBuffer; i++) {
        grid.drawRect(i * slotWidth, 0, 1, height - slotHeight * 8);
    }
    app.stage.addChild(grid);
    return grid;
}

function build_slots() {
    const available_color = 0x343464;
    const unavailable_color = 0x2f2f2f;
    const numDaysWithBuffer = 6;
    const numSlotsWithBuffer = 56;
    const slotWidth = width / numDaysWithBuffer;
    const slotHeight = height / numSlotsWithBuffer;
    var slotGrid = new PIXI.Graphics();
    slotGrid.x = 20;
    slotGrid.y = 4 * slotHeight;
    slotGrid.beginFill(available_color);
    // Vertical Lines
    for (var i = 0; i < 5 * 48; i++) {
        slotGrid.drawRect(Math.floor(i / 48) * slotWidth, i % 48 * slotHeight, slotWidth, slotHeight);
        
    }
    
    app.stage.addChild(slotGrid);
    return slotGrid;
}

function build_square(id) {
    var square = new PIXI.Graphics();
    square.beginFill(rect_color);
    square.drawRect(0, 0, 100, 100);
    square.x = 50 + 150 * id;
    square.y = 50;
    square.interactive = true;
    square.id = id;

    app.stage.addChild(square);

    square.on('mousedown', pointer_down);
    square.on('mouseover', pointer_over);
    square.on('mouseup', pointer_up);

    return square;
}

function pointer_down() {
    this.clear();
    color = Math.random() * 0xffffff;
    this.beginFill(color);
    this.drawRect(0, 0, 100, 100);
    this.x += 1;
    this.y += 1;
    mouse_down = true;
}

function pointer_up() {
    console.log("pointer up");
    mouse_down = false;
}

function pointer_over() {
    console.log(`I am square ${this.id}`);
    if (this.id == 2 && mouse_down) {
        this.clear();
        this.beginFill(color);
        this.drawRect(0, 0, 100, 100);
    }
}

class Slot {
    constructor(slotNum, available) {        this.slotNum = slotNum;        this.available = available;    }

}
    // some thoughts:
    // 1) sometimes "this" is not what you think it is, if so use bind
    // 2) start simple and test, use abstraction
