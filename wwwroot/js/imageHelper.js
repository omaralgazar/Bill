window.imageHelper = {
    resize: function (dataUrl, maxWidth, quality) {
        return new Promise((resolve, reject) => {
            const img = new Image();
            img.onload = function () {
                let width = img.width;
                let height = img.height;

                if (width > maxWidth) {
                    height = Math.round(height * (maxWidth / width));
                    width = maxWidth;
                }

                const canvas = document.createElement('canvas');
                canvas.width = width;
                canvas.height = height;

                const ctx = canvas.getContext('2d');
                ctx.drawImage(img, 0, 0, width, height);

                resolve(canvas.toDataURL('image/jpeg', quality));
            };
            img.onerror = function () {
                reject('تعذر تحميل الصورة');
            };
            img.src = dataUrl;
        });
    }
};
