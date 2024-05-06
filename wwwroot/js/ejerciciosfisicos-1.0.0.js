window.onload = ListadoEjerciciosFisicos;

function ListadoEjerciciosFisicos() {
    $.ajax({
        url: '../../EjerciciosFisicos/ListadoEjerciciosFisicos',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function (ejerciciosFisicos) {
            let contenidoTabla = '';
            $.each(ejerciciosFisicos, function (index, ejerciciosfisico) {
                // Llamada a la función "ObtenerDescripcionEjercicio" con método GET
                $.ajax({
                    url: '../../EjerciciosFisicos/ObtenerDescripcionEjercicio/' + ejerciciosfisico.tipoEjercicioID,
                    type: 'GET', 
                    dataType: 'json', 
                    success: function (data) {
                        var descripcion = data.descripcion;
                        contenidoTabla += `
                            <tr>
                                <td>${descripcion}</td>
                                <td>${ejerciciosfisico.inicio}</td>
                                <td>${ejerciciosfisico.fin}</td>
                                <td>${ejerciciosfisico.estadoEmocionalInicio}</td>
                                <td>${ejerciciosfisico.estadoEmocionalFin}</td>
                                <td>${ejerciciosfisico.observaciones ? ejerciciosfisico.observaciones : '-'}</td>
                                <td>
                                    <button type="button" class="btn btn-success" onclick="AbrirModalEditar(${ejerciciosfisico.ejercicioFisicoID})">Editar</button>
                                </td>
                                <td>
                                    <button type="button" class="btn btn-danger" onclick="EliminarActividad(${ejerciciosfisico.ejercicioFisicoID})">Eliminar</button>
                                </td>
                            </tr>`;
                        $("#tbody-tipodDeEjercicios").html(contenidoTabla);
                    },
                    error: function (xhr, status) {
                        alert('Disculpe, existió un problema al cargar la descripción del ejercicio');
                    }
                });
            });
        },
        error: function (xhr, status) {
            alert('Disculpe, existió un problema al cargar los ejercicios fisicos');
        }
    });
}


function GuardarEjercicio() {
    // Obtener los valores del formulario
    var tipoEjercicioID = $("#TipoEjercicioID").val();
    var inicio = $("#FechaInicio").val();
    var fin = $("#FechaFin").val();
    var estadoEmocionalInicio = $("#EstadoEmocionalInicio").val();
    var estadoEmocionalFin = $("#EstadoEmocionalFin").val();
    var observaciones = $("#Observaciones").val();

    // Realizar la llamada AJAX para guardar el ejercicio
    $.ajax({
        url: '../../EjerciciosFisicos/GuardarEjercicio',
        type: 'POST',
        data: {
            TipoEjercicioID: tipoEjercicioID,
            Inicio: inicio,
            Fin: fin,
            EstadoEmocionalInicio: estadoEmocionalInicio,
            EstadoEmocionalFin: estadoEmocionalFin,
            Observaciones: observaciones
        },
        success: function (response) {
            // Verificar si la operación fue exitosa
            if (response.success) {
                // Mostrar un mensaje de éxito
                alert(response.message);
                // Limpiar el formulario y cerrar el modal
                LimpiarModal();
                $("#ModalEjercicioFisico").modal('hide');
            } else {
                // Mostrar un mensaje de error si la operación falla
                alert('Error al guardar el ejercicio: ' + response.message);
            }
        },
        error: function (xhr, status) {
            // Mostrar un mensaje de error en caso de que la llamada AJAX falle
            alert('Error al guardar el ejercicio: ' + status);
        }
    });
}

function LimpiarModal() {
    // Limpiar el valor del TipoEjercicioID
    $("#TipoEjercicioID").val(0);
    // Limpiar los campos de fecha
    $("#FechaInicio").val('');
    $("#FechaFin").val('');
    // Limpiar los campos de estado emocional
    $("#EstadoEmocionalInicio").val(0);
    $("#EstadoEmocionalFin").val(0);
    // Limpiar el campo de observaciones
    $("#Observaciones").val('');
}
