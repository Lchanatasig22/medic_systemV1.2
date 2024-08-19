$(document).ready(function () {
    // Añadir fila a la tabla de diagnósticos
    $('#anadirFila').on('click', function () {
        const $diagnosticoSelect = $('#DiagnosticoId');
        const selectedDiagnostico = $diagnosticoSelect.val();
        const selectedDiagnosticoText = $diagnosticoSelect.find("option:selected").text();

        if (selectedDiagnostico) {
            const newRow = `
            <tr>
                <td>
                    <div class="input-group">
                        <select name="Diagnosticos.DiagnosticoId" class="form-control">
                            <option value="${selectedDiagnostico}" selected>${selectedDiagnosticoText}</option>
                        </select>
                    </div>
                </td>
                <td>
                    <div class="btn-group btn-group-toggle" data-toggle="buttons">
                        <label class="btn btn-outline-secondary">
                            <input type="checkbox" name="Diagnosticos.PresuntivoDiagnosticos" autocomplete="off"> Presuntivo
                        </label>
                        <label class="btn btn-outline-secondary">
                            <input type="checkbox" name="Diagnosticos.DefinitivoDiagnosticos" autocomplete="off"> Definitivo
                        </label>
                    </div>
                </td>
                <td><button type="button" class="btn btn-outline-secondary eliminar-fila-diagnostico"><i class="fas fa-times-circle"></i> Eliminar</button></td>
            </tr>`;

            $('#diagnosticoTableBody').append(newRow);
            $diagnosticoSelect.val(''); // Resetea el select después de añadir
        } else {
            alert("Por favor, seleccione un diagnóstico antes de añadir.");
        }
    });

    // Eliminar fila de diagnóstico
    $('#diagnosticoTableBody').on('click', '.eliminar-fila-diagnostico', function () {
        $(this).closest('tr').remove();
    });

    // Añadir fila a la tabla Medicamentos
    $('#anadirFilaMedicamento').on('click', function () {
        const $medicamentoSelect = $('#MedicamentoId');
        const selectedMedicamento = $medicamentoSelect.val();
        const selectedMedicamentoText = $medicamentoSelect.find("option:selected").text();

        if (selectedMedicamento) {
            const newRow = `
            <tr>
                <td>
                    <div class="input-group">
                        <select name="Medicamentos.MedicamentoId" class="form-control">
                            <option value="${selectedMedicamento}" selected>${selectedMedicamentoText}</option>
                        </select>
                    </div>
                </td>
                <td><input type="number" name="Medicamentos.Cantidad" max="999" placeholder="0" class="form-control" /></td>
                <td><input type="text" name="Medicamentos.Observacion" maxlength="300" placeholder="Máximo 300 caracteres" class="form-control" /></td>
                <td><button type="button" class="btn btn-outline-secondary eliminar-fila-medicamento"><i class="fas fa-times-circle"></i> Eliminar</button></td>
            </tr>`;

            $('#medicamentosTableBody').append(newRow);
            $medicamentoSelect.val(''); // Resetea el select después de añadir
        } else {
            alert("Por favor, seleccione un medicamento antes de añadir.");
        }
    });

    // Eliminar fila de medicamentos
    $('#medicamentosTableBody').on('click', '.eliminar-fila-medicamento', function () {
        $(this).closest('tr').remove();
    });

    // Añadir fila a la tabla Imagenes
    $('#anadirFilaImagen').on('click', function () {
        const $imagenSelect = $('#ImagenId');
        const selectedImagen = $imagenSelect.val();
        const selectedImagenText = $imagenSelect.find("option:selected").text();

        if (selectedImagen) {
            const newRow = `
            <tr>
                <td>${selectedImagenText}<input type="hidden" name="Imagenes.ImagenId" value="${selectedImagen}" /></td>
                <td><input type="number" name="Imagenes.Cantidad" max="999" placeholder="0" class="form-control" /></td>
                <td><input type="text" name="Imagenes.Observacion" maxlength="300" placeholder="Máximo 300 caracteres" class="form-control" /></td>
                <td><button type="button" class="btn btn-outline-secondary eliminar-fila-imagen"><i class="fas fa-times-circle"></i> Eliminar</button></td>
            </tr>`;

            $('#imagenesTableBody').append(newRow);
            $imagenSelect.val(''); // Resetea el select después de añadir
        } else {
            alert("Por favor, seleccione una imagen (examen) antes de añadir.");
        }
    });

    // Eliminar fila de imágenes
    $('#imagenesTableBody').on('click', '.eliminar-fila-imagen', function () {
        $(this).closest('tr').remove();
    });

    // Añadir fila a la tabla Laboratorios
    $('#anadirFilaLaboratorio').on('click', function () {
        const $laboratorioSelect = $('#LaboratorioId');
        const selectedLaboratorio = $laboratorioSelect.val();
        const selectedLaboratorioText = $laboratorioSelect.find("option:selected").text();

        if (selectedLaboratorio) {
            const newRow = `
            <tr>
                <td>
                    <div class="input-group">
                        <select name="Laboratorios.LaboratorioId" class="form-control">
                            <option value="${selectedLaboratorio}" selected>${selectedLaboratorioText}</option>
                        </select>
                    </div>
                </td>
                <td><input type="number" name="Laboratorios.Cantidad" max="999" placeholder="0" class="form-control" /></td>
                <td><input type="text" name="Laboratorios.Observacion" maxlength="300" placeholder="Máximo 300 caracteres" class="form-control" /></td>
                <td><button type="button" class="btn btn-outline-secondary eliminar-fila-laboratorio"><i class="fas fa-times-circle"></i> Eliminar</button></td>
            </tr>`;

            $('#laboratorioTableBody').append(newRow);
            $laboratorioSelect.val(''); // Resetea el select después de añadir
        } else {
            alert("Por favor, seleccione un laboratorio antes de añadir.");
        }
    });

    // Eliminar fila de laboratorios
    $('#laboratorioTableBody').on('click', '.eliminar-fila-laboratorio', function () {
        $(this).closest('tr').remove();
    });

    // Función para enviar el formulario como JSON
    $('#submitFormButton').on('click', function () {
        submitFormAsJson();
    });

    // Configuración inicial del wizard
    const $navListItems = $('div.stepwizard-step button');
    const $allWells = $('.setup-content');
    $allWells.hide();

    // Mostrar el primer paso al cargar la página
    $('#step-1').show();
    const $firstNavItem = $navListItems.first();
    $firstNavItem.addClass('btn-primary').removeClass('btn-secondary');
    $firstNavItem.removeAttr('disabled');  // Asegúrate de que el primer botón no esté deshabilitado

    // Manejo de clicks en los pasos del wizard
    $navListItems.on('click', function (e) {
        e.preventDefault();
        const $item = $(this);
        const step = $item.data('step');
        const $target = $('#step-' + step);

        if (!$item.hasClass('disabled')) {
            $navListItems.removeClass('btn-primary').addClass('btn-secondary');
            $item.addClass('btn-primary');
            $allWells.hide();
            $target.show().find('input:eq(0)').focus();
        }
    });

    // Mostrar u ocultar campos de observación al cambiar los switches
    $('.consulta-antecedente-checked').on('change', function () {
        const $observacionField = $(this).closest('.fields').find('.consulta-antecedente-observacion');
        const isChecked = $(this).is(':checked');

        $observacionField.toggle(isChecked);
        $observacionField.find('input').prop('disabled', !isChecked);
    });
});

function goToNextStep(stepNumber, event) {
    event = event || window.event;
    var curStep = $(event.target).closest(".setup-content"),
        nextStepWizard = $('div.stepwizard-step button[data-step="' + stepNumber + '"]'),
        curInputs = curStep.find("input, select"),
        isValid = true;

    $(".form-group").removeClass("has-error");
    curInputs.each(function () {
        if (!this.validity.valid) {
            isValid = false;
            $(this).closest(".form-group").addClass("has-error");
        }
    });

    if (isValid) {
        curStep.hide();
        nextStepWizard.removeAttr('disabled').trigger('click');
        $('#step-' + stepNumber).show();
    }
}

function goToPreviousStep(stepNumber) {
    var curStep = $('#step-' + stepNumber),
        prevStepWizard = $('div.stepwizard-step button[data-step="' + (stepNumber - 1) + '"]');

    prevStepWizard.removeAttr('disabled').trigger('click');
    curStep.hide();
    $('#step-' + (stepNumber - 1)).show();
}


function submitFormAsJson() {
    const form = document.getElementById('consultationForm');
    const formData = new FormData(form);
    const object = {};
    formData.forEach((value, key) => {
        object[key] = value;
    });

    // Capturar los diagnósticos
    const diagnosticos = [];
    document.querySelectorAll('#diagnosticoTableBody tr').forEach(row => {
        const diagnosticoIdElement = row.querySelector('select[name="Diagnosticos.DiagnosticoId"]');
        if (diagnosticoIdElement) {
            const diagnosticoId = diagnosticoIdElement.value;
            const presuntivoElement = row.querySelector('input[name="Diagnosticos.PresuntivoDiagnosticos"]');
            const definitivoElement = row.querySelector('input[name="Diagnosticos.DefinitivoDiagnosticos"]');
            const presuntivo = presuntivoElement ? presuntivoElement.checked : false;
            const definitivo = definitivoElement ? definitivoElement.checked : false;
            diagnosticos.push({
                diagnostico_id: diagnosticoId,
                presuntivo_diagnostico: presuntivo,
                definitivo_diagnostico: definitivo
            });
        }
    });
    object["Diagnosticos"] = diagnosticos;

    // Capturar los medicamentos
    const medicamentos = [];
    document.querySelectorAll('#medicamentosTableBody tr').forEach(row => {
        const medicamentoIdElement = row.querySelector('select[name="Medicamentos.MedicamentoId"]');
        if (medicamentoIdElement) {
            const medicamentoId = medicamentoIdElement.value;
            const cantidadElement = row.querySelector('input[name="Medicamentos.Cantidad"]');
            const observacionElement = row.querySelector('input[name="Medicamentos.Observacion"]');
            const cantidad = cantidadElement ? cantidadElement.value : "";
            const observacion = observacionElement ? observacionElement.value : "";
            medicamentos.push({
                medicamento_id: medicamentoId,
                cantidad_medicamento: cantidad,
                observacion_medicamento: observacion
            });
        }
    });
    object["Medicamentos"] = medicamentos;

    // Capturar las imágenes
    const imagenes = [];
    document.querySelectorAll('#imagenesTableBody tr').forEach(row => {
        const imagenIdElement = row.querySelector('input[name="Imagenes.ImagenId"]');
        if (imagenIdElement) {
            const imagenId = imagenIdElement.value;
            const cantidadElement = row.querySelector('input[name="Imagenes.Cantidad"]');
            const observacionElement = row.querySelector('input[name="Imagenes.Observacion"]');
            const cantidad = cantidadElement ? cantidadElement.value : "";
            const observacion = observacionElement ? observacionElement.value : "";
            imagenes.push({
                imagen_id: imagenId,
                cantidad_imagen: cantidad,
                observacion_imagen: observacion
            });
        }
    });
    object["Imagenes"] = imagenes;

    // Capturar los laboratorios
    const laboratorios = [];
    document.querySelectorAll('#laboratorioTableBody tr').forEach(row => {
        const laboratorioIdElement = row.querySelector('select[name="Laboratorios.LaboratorioId"]');
        if (laboratorioIdElement) {
            const laboratorioId = laboratorioIdElement.value;
            const cantidadElement = row.querySelector('input[name="Laboratorios.Cantidad"]');
            const observacionElement = row.querySelector('input[name="Laboratorios.Observacion"]');
            const cantidad = cantidadElement ? cantidadElement.value : "";
            const observacion = observacionElement ? observacionElement.value : "";
            laboratorios.push({
                laboratorio_id: laboratorioId,
                cantidad_laboratorio: cantidad,
                observacion_laboratorio: observacion
            });
        }
    });
    object["Laboratorios"] = laboratorios;

    // Convertir el objeto a JSON y enviarlo
    const json = JSON.stringify(object);

    fetch('/Consultation/CrearConsulta', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: json
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            throw new Error('Something went wrong');
        })
        .then(data => {
            alert('Consulta creada con ID: ' + data.Id);
        })
        .catch(error => {
            console.error('Error:', error);
        });
}


