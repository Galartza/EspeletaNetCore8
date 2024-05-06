window.onload = ListadoEjerciciosFisicos;

function ListadoEjerciciosFisicos() {
    $.ajax({
        url: '../../EjerciciosFisicos/ListadoEjerciciosFisicos',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function (ejerciciosFisicos) {
            let promises = [];
            let contenidoTabla = '';

            ejerciciosFisicos.forEach(ejercicioFisico => {
                // Create an array of promises for all the description calls
                promises.push(
                    new Promise((resolve, reject) => {
                        $.ajax({
                            url: '../../EjerciciosFisicos/ObtenerDescripcionEjercicio/' + ejercicioFisico.tipoEjercicioID,
                            type: 'GET',
                            dataType: 'json',
                            success: function (data) {
                                var descripcion = data.descripcion;
                                contenidoTabla += `
                                    <tr>
                                        <td>${descripcion}</td>
                                        <td>${ejercicioFisico.inicio}</td>
                                        <td>${ejercicioFisico.fin}</td>
                                        <td>${ejercicioFisico.estadoEmocionalInicio}</td>
                                        <td>${ejercicioFisico.estadoEmocionalFin}</td>
                                        <td>${ejercicioFisico.observaciones || '-'}</td>
                                        <td>
                                            <button type="button" class="btn btn-success" onclick="AbrirModalEditar(${ejercicioFisico.ejercicioFisicoID})">Editar</button>
                                        </td>
                                        <td>
                                            <button type="button" class="btn btn-danger" onclick="EliminarActividad(${ejercicioFisico.ejercicioFisicoID})">Eliminar</button>
                                        </td>
                                    </tr>`;
                                resolve();
                            },
                            error: function (xhr, status) {
                                reject('Disculpe, existió un problema al cargar la descripción del ejercicio');
                            }
                        });
                    })
                );
            });

            // Wait for all promises to resolve before updating the DOM
            Promise.all(promises)
                .then(() => {
                    $("#tbody-tipodDeEjercicios").html(contenidoTabla);
                })
                .catch(error => {
                    alert(error);
                });
        },
        error: function (xhr, status) {
            alert('Disculpe, existió un problema al cargar los ejercicios físicos');
        }
    });
}
