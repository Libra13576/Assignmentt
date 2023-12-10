function ChangeImage(UploadImage, previewImg) {
    if (UploadImage.file && UploadImage.file[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImg).attr('src', target.result);
        }
        reader.readAsDataURL(UploadImage.File[0]);
    }
}
