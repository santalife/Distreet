    FilePond.registerPlugin(FilePondPluginImagePreview, FilePondPluginFileEncode, FilePondPluginFileMetadata, FilePondPluginImageCrop, FilePondPluginImageTransform);
    const profilepictureElem = document.querySelector('#input-profile-picture');
    const profilepicturePond= FilePond.create(profilepictureElem);

    profilepicturePond.setOptions({
        maxFiles: 1,
        labelIdle: 'Drag & Drop your Photos or <span class="filepond--label-action">Browse</span>',
        allowImageCrop: true,
        allowImageTransform: true,
        imageCropAspectRatio: '1:1',
    });
    
    var profilepicture = document.querySelector('#ProfilePicture')
    console.log(profilepicture.value)
    profilepicturePond.addFile(profilepicture.value)
    
    const documentsElem = document.querySelector('#input-documents');
    const documentsPond= FilePond.create(documentsElem);

    documentsPond.setOptions({
        maxFiles: 10,
        allowMultiple: true,
        allowReorder: true,
        labelIdle: 'Drag & Drop your Documents or <span class="filepond--label-action">Browse</span>',
        allowImageCrop: true,
        allowImageTransform: true,
        imageCropAspectRatio: '1:1',
    });
