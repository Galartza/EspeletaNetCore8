function ListadoEjerciciosFisicos() {
    $.ajax({
        url: '../../EjerciciosFisicos/ListadoEjerciciosFisicos',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function (ejercicioFisicoMostrar) {

            let contenidoTabla = '';
            $.each(ejercicioFisicoMostrar, function (index, ejercicioFisicoMostrar) {
                contenidoTabla += `
                    <tr>
                        <td>${ejercicioFisicoMostrar.tipoEjercicioDescripcion}</td>
                        <td>${ejercicioFisicoMostrar.fechaInicioString}</td>
                        <td>${ejercicioFisicoMostrar.fechaFinString}</td>
                        <td>${ejercicioFisicoMostrar.estadoEmocionalInicio}</td>
                        <td>${ejercicioFisicoMostrar.estadoEmocionalFin}</td>
                        <td>${ejercicioFisicoMostrar.observaciones}</td>
                        <td>
                            <button type="button" class="btn btn-success" onclick="AbrirModalEditar(${ejercicioFisicoMostrar.ejercicioFisicoID})">Editar</button>
                        </td>
                        <td>
                            <button type="button" class="btn btn-danger" onclick="EliminarRegistro(${ejercicioFisicoMostrar.ejercicioFisicoID})">Eliminar</button>
                        </td>
                    </tr>`;
            });
            $("#tbody-tipodDeEjercicios").html(contenidoTabla);
        },
        error: function (xhr, status) {
            alert('Disculpe, existió un problema al cargar los datos');
        }
    });
}


// function AbrirModalEditar(ejercicioFisicoID) {
//     $.ajax({
//         url: '../../EjercicioFisico/TraerEjerciciosFisicos',
//         data: {ejercicioFisicoID : ejercicioFisicoID},
//         type : 'POST',
//         dataType : 'json',
//         success: function (ejerciciosfisico) {
//             let ejerciciosFisicos = ejercicioFisico[0];

//             document.getElementById("EjercicioFisicoID").value = ejerciciosFisicos.ejercicioFisicoID;
//             document.getElementById("FechaInicio").value = ejerciciosFisicos.inicio;
//             document.getElementById("FechaFin").value = ejerciciosFisicos.fin;
//             document.getElementById("EstadoEmocionalInicio").value = ejerciciosFisicos.estadoEmocionalInicio;
//             document.getElementById("EstadoEmocionalFin").value = ejerciciosFisicos.estadoEmocionalFin;
//             document.getElementById("Observaciones").value = ejerciciosFisicos.observaciones;
//             $("#ModalEjercicioFisico").modal("show");
//         },
//         error: function (xhr, status) {
//             console.log('Disculpe, existió un problema al consultar el registro para ser modificado.');
//         }
//     });
// }

// function GuardarRegistro() {

//     let ejercicioFisicoID = document.getElementById("EjercicioFisicoID").value;
//     let tipoEjercicioID = document.getElementById("TipoEjercicioID").value;
//     let inicio = document.getElementById("FechaInicio").value;
//     let fin = document.getElementById("FechaFin").value;
//     let estadoEmocionalInicio = document.getElementById("EstadoEmocionalInicio").value;
//     let estadoEmocionalFin = document.getElementById("EstadoEmocionalFin").value;
//     let observaciones = document.getElementById("Observaciones").value;


//     $.ajax({
//         url: '../EjercicioFisico/GuardarRegistro',
//         data: {
//             ejercicioFisicoID : ejercicioFisicoID,
//             tipoEjercicioID : tipoEjercicioID,
//             inicio : inicio,
//             fin : fin,
//             estadoEmocionalInicio : estadoEmocionalInicio,
//             estadoEmocionalFin : estadoEmocionalFin,
//             observaciones : observaciones
//         },
//         type: 'POST',
//         dataType: 'json',
//         success: function (response) {
//             if (response.resultado !== "") {
//                 alert(response.resultado);
//                 ListadoEjerciciosFisicos();
//             } else {
//                 alert('No se recibió un mensaje de resultado');
//             }
//         },
//     });
//     $("ModalEjercicioFisico").modal("hide");
// }