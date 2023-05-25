
Dropzone.options.myDropzone = {
    url: 'Property/AddProperty',
    autoProcessQueue: false,
    uploadMultiple: true,
    parallelUploads: 5,
    maxFiles: 10,
    maxFilesize: 1,
    acceptedFiles: 'image/*',
    addRemoveLinks: true,
    init: function () {
        var dzClosure = this;

        // Handle the form submission
        document.getElementById("submit-all").addEventListener("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            dzClosure.processQueue();
        });

        // Customize the look and feel of the dropzone area
        this.on("sendingmultiple", function (data, xhr, formData) {
            // Get all input elements within the form
            var inputs = jQuery("#formular :input");

            // Iterate over the inputs and append their values to the formData object
            inputs.each(function () {
                var fieldName = $(this).attr("name");
                var fieldValue = $(this).val();
                formData.append(fieldName, fieldValue);
            });
        });
        this.on("success", function (file, response) {
            window.location.href = response.redirectToUrl;
        });
    }
};

// Disable autoDiscover and manually initialize Dropzone
Dropzone.autoDiscover = false;
    var myDropzone = new Dropzone("#myDropzone");
