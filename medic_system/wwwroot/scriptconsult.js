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
                        <select name="ConsultaDiagnostico.DiagnosticoId" class="form-control">
                            <option value="${selectedDiagnostico}" selected>${selectedDiagnosticoText}</option>
                        </select>
                    </div>
                </td>
                <td>
                    <div class="btn-group btn-group-toggle" data-toggle="buttons">
                        <label class="btn btn-outline-secondary">
                            <input type="checkbox" name="ConsultaDiagnostico.PresuntivoDiagnosticos[]" autocomplete="off"> Presuntivo
                        </label>
                        <label class="btn btn-outline-secondary">
                            <input type="checkbox" name="ConsultaDiagnostico.DefinitivoDiagnosticos[]" autocomplete="off"> Definitivo
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
                        <select name="ConsultaMedicamento.MedicamentoId" class="form-control">
                            <option value="${selectedMedicamento}" selected>${selectedMedicamentoText}</option>
                        </select>
                    </div>
                </td>
                <td><input type="number" name="cantidadMedicamentos[]" max="999" placeholder="0" class="form-control" /></td>
                <td><input type="text" name="observacionMedicamentos[]" maxlength="300" placeholder="Máximo 300 caracteres" class="form-control" /></td>
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

    // Añadir fila a la tabla Imagen 
    $('#anadirFilaImagen').on('click', function () {
        const $imagenSelect = $('#ImagenId');
        const selectedImagen = $imagenSelect.val();
        const selectedImagenText = $imagenSelect.find("option:selected").text();

        if (selectedImagen) {
            const newRow = `
            <tr>
                <td>${selectedImagenText}<input type="hidden" name="imagenes[]" value="${selectedImagen}" /></td>
                <td><input type="number" name="cantidadImagenes[]" max="999" placeholder="0" class="form-control" /></td>
                <td><input type="text" name="observacionImagenes[]" maxlength="300" placeholder="Máximo 300 caracteres" class="form-control" /></td>
                <td><button type="button" class="btn btn-outline-secondary eliminar-fila-imagen"><i class="fas fa-times-circle"></i> Eliminar</button></td>
            </tr>`;

            $('#imagenesTableBody').append(newRow);
            $imagenSelect.val(''); // Resetea el select después de añadir
        } else {
            alert("Por favor, seleccione una imagen (examen) antes de añadir.");
        }
    });

    // Eliminar fila de imágenes (exámenes)
    $('#imagenesTableBody').on('click', '.eliminar-fila-imagen', function () {
        $(this).closest('tr').remove();
    });

    // Añadir fila a la tabla laboratorio
    $('#anadirFilaLaboratorio').on('click', function () {
        const $laboratorioSelect = $('#LaboratorioId');
        const selectedLaboratorio = $laboratorioSelect.val();
        const selectedLaboratorioText = $laboratorioSelect.find("option:selected").text();

        if (selectedLaboratorio) {
            const newRow = `
            <tr>
                <td>
                    <div class="input-group">
                        <select name="ConsultaLaboratorio.CatalogoLaboratorioId" class="form-control">
                            <option value="${selectedLaboratorio}" selected>${selectedLaboratorioText}</option>
                        </select>
                    </div>
                </td>
                <td><input type="number" name="cantidadLaboratorios[]" max="999" placeholder="0" class="form-control" /></td>
                <td><input type="text" name="observacionLaboratorios[]" maxlength="300" placeholder="Máximo 300 caracteres" class="form-control" /></td>
                <td><button type="button" class="btn btn-outline-secondary eliminar-fila-laboratorio"><i class="fas fa-times-circle"></i> Eliminar</button></td>
            </tr>`;

            $('#laboratorioTableBody').append(newRow);
            $laboratorioSelect.val(''); // Resetea el select después de añadir
        } else {
            alert("Por favor, seleccione un laboratorio antes de añadir.");
        }
    });

    // Eliminar fila de laboratorio
    $('#laboratorioTableBody').on('click', '.eliminar-fila-laboratorio', function () {
        $(this).closest('tr').remove();
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

    // Función para manejar el botón "Siguiente"
    function goToNextStep(event) {
        const $curStep = $(event.target).closest(".setup-content");
        const curStepId = $curStep.attr("id").split('-')[1];
        const $nextStepWizard = $('div.stepwizard-step button[data-step="' + (parseInt(curStepId) + 1) + '"]');
        const $curInputs = $curStep.find("input[type='text'],input[type='url']");
        let isValid = true;

        $(".form-group").removeClass("has-error");

        $curInputs.each(function () {
            if (!this.validity.valid) {
                isValid = false;
                $(this).closest(".form-group").addClass("has-error");
            }
        });

        if (isValid) {
            $nextStepWizard.removeAttr('disabled').trigger('click');
            $curStep.hide();
            $('#step-' + (parseInt(curStepId) + 1)).show();
        }
    }

    // Manejo del botón "Regresar"
    function goToPreviousStep(event) {
        const $curStep = $(event.target).closest(".setup-content");
        const curStepId = $curStep.attr("id").split('-')[1];
        const $prevStepWizard = $('div.stepwizard-step button[data-step="' + (parseInt(curStepId) - 1) + '"]');

        $prevStepWizard.addClass('btn-primary');
        $curStep.hide();
        $('#step-' + (parseInt(curStepId) - 1)).show();
    }

    // Mostrar u ocultar campos de observación al cambiar los switches
    $('.consulta-antecedente-checked').on('change', function () {
        const $observacionField = $(this).closest('.fields').find('.consulta-antecedente-observacion');
        const isChecked = $(this).is(':checked');

        $observacionField.toggle(isChecked);
        $observacionField.find('input').prop('disabled', !isChecked);
    });
});
