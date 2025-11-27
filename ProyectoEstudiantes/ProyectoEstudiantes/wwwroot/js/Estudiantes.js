// Aislar funciones 
(() => {
    const Estudiantes = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },
        inicializarTabla() {
            this.tabla = $('#tablaEstudiantes').DataTable({
                ajax: {
                    url: '/Estudiante/ObtenerEstudiantes',
                    type: 'GET',
                    dataSrc: 'data',
                    error: function (exceptionResponse) {
                        Swal.fire({
                            title: 'Error',
                            text: exceptionResponse.responseJSON.mensaje,
                            icon: 'error',
                        });
                    }
                },
                columns: [
                    { data: 'id', title: 'ID' },
                    { data: 'nombre', title: 'Nombre' },
                    { data: 'apellido', title: 'Apellido' },
                    { data: 'edad', title: 'Edad' },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-sm btn-primary editar" data-id="${row.id}">Editar</button>
                                <button class="btn btn-sm btn-danger eliminar" data-id="${row.id}">Eliminar</button>
                            `;
                        }
                    }
                ],
                responsive: true,
                processing: true,
                pageLength: 10
            });
        },
        registrarEventos() {

            $('#tablaEstudiantes').on('click', '.editar', function () {
                const id = $(this).data('id');
                Estudiantes.CargaDatosEstudiante(id);
            });

            $('#tablaEstudiantes').on('click', '.eliminar', function () {
                const id = $(this).data('id');
                Estudiantes.EliminarEstudiante(id);
            });

            $('#btnGuardarCambios').on('click', function () {
                Estudiantes.GuardarEstudiante();
            });

            $('#btnEditarCambios').on('click', function () {
                Estudiantes.EditarEstudiante();
            });

            $('#btnBuscar').on('click', function () {
                Estudiantes.BuscarPorId();
            });

        },
        GuardarEstudiante: function () {
            let form = $('#formCrearEstudiante');

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'), //ACTION DEL CONTROLADOR QUE DEBE EJECUTARSE, ESTA EN EL FORMULARIO
                type: 'POST', //TIPO DE REQUEST
                data: form.serialize(), //SERIALIZAR EL FORMULARIO
                success: function (response) { //MANEJO DE RESPUESTA
                    if (!response.esError) {
                        $('#modalCrearEstudiante').modal('hide'); // Ocultar el modal
                        Estudiantes.tabla.ajax.reload(); //Recargar la tabla
                        form[0].reset(); //Borrar el formulario

                        Swal.fire({
                            title: 'Éxito',
                            text: response.mensaje,
                            icon: 'success',
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Error',
                            text: response.mensaje,
                            icon: 'error',
                        });
                    }

                },
                error: function (exceptionResponse) {
                    Swal.fire({
                        title: 'Error',
                        text: exceptionResponse.responseJSON.mensaje,
                        icon: 'error',
                    });
                }
            });
        },

        CargaDatosEstudiante: function (id) {
            $.get(`/Estudiante/ObtenerEstudiantePorId/${id}`, function (response) {
                if (!response.esError) {
                    const estudiante = response.data;
                    $('#EstudianteId').val(estudiante.id);
                    $('#Nombre').val(estudiante.nombre);
                    $('#Apellido').val(estudiante.apellido);
                    $('#Edad').val(estudiante.edad);
                    $('#modalEditarEstudiante').modal('show');
                } else {
                    Swal.fire({
                        title: 'Error',
                        text: response.mensaje,
                        icon: 'error',
                    });
                }
            });

        },

        EditarEstudiante: function () {
            let form = $('#formEditarEstudiante');

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (!response.esError) {
                        $('#modalEditarEstudiante').modal('hide');
                        Estudiantes.tabla.ajax.reload();
                        Swal.fire({
                            title: 'Éxito',
                            text: response.mensaje,
                            icon: 'success',
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Error',
                            text: response.mensaje,
                            icon: 'error',
                        });
                    }
                },
                error: function (exceptionResponse) {
                    Swal.fire({
                        title: 'Error',
                        text: exceptionResponse.responseJSON.mensaje,
                        icon: 'error',
                    });
                }
            });
        },

        EliminarEstudiante: function (id) {
            Swal.fire({
                title: "Estas seguro?",
                text: "No podras revertir esta acción",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Si, borrar"
            }).then((result) => {

                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Estudiante/EliminarEstudiante',
                        type: 'POST',
                        data: { id: id },
                        success: function (response) {
                            if (!response.esError) {
                                Estudiantes.tabla.ajax.reload();
                                Swal.fire({
                                    title: 'Éxito',
                                    text: response.mensaje,
                                    icon: 'success',
                                });
                            }
                            else {
                                Swal.fire({
                                    title: 'Error',
                                    text: response.mensaje,
                                    icon: 'error',
                                });
                            }
                        },
                        error: function (exceptionResponse) {
                            Swal.fire({
                                title: 'Error',
                                text: exceptionResponse.responseJSON.mensaje,
                                icon: 'error',
                            });
                        }
                    });
                }
            });
        },

        BuscarPorId: function () {

            const id = $('#buscarId').val();

            if (!id) {
                Swal.fire({
                    title: 'Advertencia',
                    text: 'Debe ingresar un ID',
                    icon: 'warning'
                });
                return;
            }

            $.ajax({
                url: `/Estudiante/ObtenerEstudiantePorId/${id}`,
                type: 'GET',
                success: function (response) {
                    if (response.esError || !response.data) {
                        $('#resultado').hide();
                        Swal.fire({
                            title: 'No encontrado',
                            text: 'No existe un estudiante con ese ID',
                            icon: 'error'
                        });
                        return;
                    }

                    const est = response.data;

                    $('#resId').text(est.id);
                    $('#resNombre').text(est.nombre);
                    $('#resApellido').text(est.apellido);
                    $('#resEdad').text(est.edad);

                    $('#resultado').show();
                },
                error: function () {
                    Swal.fire({
                        title: 'Error',
                        text: 'No se pudo obtener la información',
                        icon: 'error'
                    });
                }
            });

        }

    }
    $(document).ready(() => Estudiantes.init());
})();