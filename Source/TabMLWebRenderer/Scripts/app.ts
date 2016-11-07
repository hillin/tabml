window.onload = () => {
    var canvas = document.getElementById("staff") as HTMLCanvasElement;
    var renderer = new TabRenderer(canvas);
    renderer.drawTitle("My Staff");
};