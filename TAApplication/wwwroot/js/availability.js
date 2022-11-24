let bg_color = 0xabcdef;
let rect_color = 0x550000;
let width = 800;
let height = 400;

var color = 0xffffff;
var mouse_down = false;

app = new PIXI.Application({ backgroundColor: bg_color });
app.renderer.resize(width, height);
$("#canvas_div").append(app.view);

var square_1 = build_square(1);
var square_2 = build_square(2);

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

    // some thoughts:
    // 1) sometimes "this" is not what you think it is, if so use bind
    // 2) start simple and test, use abstraction
