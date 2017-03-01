namespace TR {

    export class Utilities {
        static getExactBoundingRect(canvas: fabric.IStaticCanvas, target: fabric.IObject): IBoundingBox {
            let bounds = target.getBoundingRect();
            if (!canvas.contains(target))
                return bounds;

            bounds.left = Math.floor(bounds.left);
            bounds.top = Math.floor(bounds.top);
            bounds.width = Math.ceil(bounds.width);
            bounds.height = Math.ceil(bounds.height);

            // Get the pixel data from the canvas
            let data = canvas.getContext().getImageData(bounds.left, bounds.top, bounds.width, bounds.height).data;

            let first: number = null;
            let last: number = null;
            let right: number = null;
            let left: number = null;

            let bottom: number, top: number;

            // 1. get bottom
            {
                let row = bounds.height;
                while (last === null && row > 0) {
                    --row;
                    for (let column = 0; column < bounds.width; column++) {
                        if (data[row * bounds.width * 4 + column * 4 + 3]) {
                            console.log('last', row);
                            last = row + 1;
                            bottom = row + 1;
                            break;
                        }
                    }
                }
            }

            // 2. get top
            {
                let row = 0;
                var checks = [];
                while (first === null && row < last) {

                    for (let column = 0; column < bounds.width; column++) {
                        if (data[row * bounds.width * 4 + column * 4 + 3]) {
                            console.log('first', row);
                            first = row - 1;
                            top = row - 1;
                            break;
                        }
                    }
                    row++;
                }
            }

            // 3. get right
            {
                let column = bounds.width;
                while (right === null && column > 0) {
                    column--;
                    for (let row = 0; row < bounds.height; row++) {
                        if (data[row * bounds.width * 4 + column * 4 + 3]) {
                            console.log('last', row);
                            right = column + 1;
                            break;
                        }
                    }
                }
            }

            // 4. get left
            {
                let column = 0;
                while (left === null && column < right) {

                    for (let row = 0; row < bounds.height; row++) {
                        if (data[row * bounds.width * 4 + column * 4 + 3]) {
                            console.log('left', column - 1);
                            left = column;
                            break;
                        }
                    }
                    column++;
                }
            }

            bounds.left = left;
            bounds.top = top;
            bounds.width = right - left;
            bounds.height = bottom - top;

            return bounds;
        }
    }

}