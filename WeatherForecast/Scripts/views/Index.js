document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById("uploadForm");

    if (!form) return;

    const fileInput = document.getElementById('forecastCsvInput');
    const fileName = document.getElementById('fileName');
    const clearBtn = document.getElementById('clearFile');
   

    fileBox.style.display = (fileName.textContent && fileName.textContent !== "No file chosen") ? 'flex' : 'none';

    fileInput.addEventListener('change', () => {
        fileName.textContent = fileInput.files.length > 0 ? fileInput.files[0].name : 'No file selected';
        fileBox.style.display = fileInput.files.length > 0 ? 'flex' : 'none';

        if (fileInput.files.length > 0) {
            form.style.display = 'none';
            form.submit();
        }
    });

    clearBtn.addEventListener('click', (e) => {
        e.stopPropagation();
        fileInput.value = '';
        fileBox.style.display = 'none';
        fileName.textContent = 'No file chosen';
    })

    const fileUploadTrigger = document.getElementById("fileUploadTrigger");

    fileUploadTrigger?.addEventListener('click', function () {
        fileInput.click();
    });
});

function ReuploadCSV() {
    const uploadSection = document.getElementById('uploadSection');
    const forecastSection = document.getElementById('forecastSection');

    uploadSection.style.display = 'block';
    forecastSection.style.display = 'none';
    fileName.textContent = 'No file selected';
}