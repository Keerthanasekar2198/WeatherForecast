document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById("uploadForm");

    if (!form) return;

    const fileInput = document.getElementById('forecastCsvInput');
    const fileName = document.getElementById('fileName');
    const clearBtn = document.getElementById('clearFile');
    const dropZone = document.getElementById('dropZone');
   

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

    ['dragenter', 'dragover'].forEach(eventType => {
        dropZone.addEventListener(eventType, (e) => {
            e.preventDefault();
            dropZone.classList.add('dragover');
        });
    });

    ['dragleave', 'drop'].forEach(eventType => {
        dropZone.addEventListener(eventType, (e) => {
            e.preventDefault();
            dropZone.classList.remove('dragover');
        });
    });

    dropZone.addEventListener('drop', (e) => {
        e.preventDefault();
        const files = e.dataTransfer.files;

        if (files.length > 0) {
            fileInput.files = files;

            fileName.textContent = files[0].name;
            fileBox.style.display = 'flex';

            form.submit();
        }
    });
});

function ReuploadCSV() {
    const fileInput = document.getElementById('forecastCsvInput');
    fileInput.value = '';
    fileInput.click();
}

function RenderChart() {
    const uploadSection = document.getElementById('uploadSection');
    const forecastSection = document.getElementById('forecastSection');
    const chartSection = document.getElementById('chartSection');

    uploadSection.style.display = 'none';
    forecastSection.style.display = 'none';
    chartSection.style.display = 'block';
}

function ShowTable() {
    const uploadSection = document.getElementById('uploadSection');
    const forecastSection = document.getElementById('forecastSection');
    const chartSection = document.getElementById('chartSection');

    uploadSection.style.display = 'none';
    forecastSection.style.display = 'block';
    chartSection.style.display = 'none';
}

function OnLocationChange(selectedElement) {
    var selectedOption = selectedElement.options[selectedElement.selectedIndex];
    var lat = selectedOption.getAttribute("data-lat");
    var lon = selectedOption.getAttribute("data-lon")

    document.getElementById("Latitude").value = lat;
    document.getElementById("Longitude").value = lon;
    document.getElementById("cityForm").submit();
}