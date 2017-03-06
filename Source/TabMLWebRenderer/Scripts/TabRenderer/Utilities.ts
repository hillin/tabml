namespace TR {

    export class Utilities {
        static getExactBoundingRect(canvas: fabric.IStaticCanvas, target: fabric.IObject): IBoundingBox {
            let bounds = target.getBoundingRect();

            if (!canvas.contains(target))
                return bounds;

            let width = Math.ceil(bounds.width);
            let height = Math.ceil(bounds.height);

            // Get the pixel data from the canvas
            let data = canvas.getContext().getImageData(Math.floor(bounds.left), Math.floor(bounds.top), width, height).data;

            let left = 0;
            let top = 0;
            let right = width - 1;
            let bottom = height - 1;

            function isVisible(row: number, column: number): boolean {
                return data[row * width * 4 + column * 4 + 3] > 0;
            }

            // find top
            let breaked = false;
            for (let row = top; !breaked && row <= bottom; ++row) {
                for (let column = left; !breaked && column <= right; ++column) {
                    if (isVisible(row, column)) {
                        top = row;
                        breaked = true;
                    }
                }
            }

            // find bottom
            breaked = false;
            for (let row = bottom; !breaked && row >= top; --row) {
                for (let column = left; !breaked && column <= right; ++column) {
                    if (isVisible(row, column)) {
                        bottom = row;
                        breaked = true;
                    }
                }
            }

            // find left
            breaked = false;
            for (let column = left; !breaked && column <= right; ++column) {
                for (let row = top; !breaked && row <= bottom; ++row) {
                    if (isVisible(row, column)) {
                        left = column;
                        breaked = true;
                    }
                }
            }

            // find right 
            breaked = false;
            for (let column = right; !breaked && column >= left; --column) {
                for (let row = top; !breaked && row <= bottom; ++row) {
                    if (isVisible(row, column)) {
                        right = column;
                        breaked = true;
                    }
                }
            }

            return { left: left, top: top, width: right - left + 1, height: bottom - top + 1 };
        }
    }

}