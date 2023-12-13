using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using EXAMEN.Models;

namespace EXAMEN.Controllers
{
    [Route("[Controller]")]
    public class MecanicosController : Controller
    {
        private readonly String connString;

        public MecanicosController(IConfiguration config)
        {
            connString = config.GetConnectionString("DefaultConnection");

            // Se establece la cadena de conexión al iniciar el controlador.
        }

        [HttpGet]
        // Obtiene todos los mecanicos
        public async Task<IActionResult> GetMecanicos()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM mecanicos ";
                    List<MecanicoModelos> mecanico = new List<MecanicoModelos>();
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    using (var lector = await command.ExecuteReaderAsync())
                    {
                        while (await lector.ReadAsync())
                        {
                            mecanico.Add(new MecanicoModelos()
                            {
                                idMecanico = lector.GetInt32(0),
                                Nombre = lector.GetString(1),
                                Edad = lector.GetInt32(2),
                                Domicilio = lector.GetString(3),
                                Titulo = lector.GetString(4),
                                Especialidad = lector.GetString(5),
                                SueldoBase = lector.GetInt32(6),
                                GrantTitulo = lector.GetInt32(7),
                                SueldoTotal = lector.GetInt32(8)

                                // Mapea los datos del mecanico desde la base de datos al modelo.


                            });
                        }
                        return StatusCode(200, mecanico);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al procesar la solicitud.", Error = ex.Message });
            }






        }

        [HttpGet("{idMecanico}")]
        // Obtiene un mecanico por ID

        public async Task<IActionResult> GetMecanico(int idMecanico)
        {
            try
            {
                
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    await connection.OpenAsync();
                    string query = "select * FROM mecanicos WHERE idMecanico = @id";
                    MecanicoModelos mecanico = new MecanicoModelos();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                                            // Parámetro para el ID del mecanico.


                        command.Parameters.Add(new MySqlParameter("@id", idMecanico));
                        using (var lector = await command.ExecuteReaderAsync())
                        {
                            if (await lector.ReadAsync())
                            {
                                mecanico.idMecanico = lector.GetInt32(0);
                                mecanico.Nombre = lector.GetString(1);
                                mecanico.Edad = lector.GetInt32(2);
                                mecanico.Domicilio = lector.GetString(3);
                                mecanico.Titulo = lector.GetString(4);
                                mecanico.Especialidad = lector.GetString(5);
                                mecanico.SueldoBase = lector.GetInt32(6);
                                mecanico.GrantTitulo = lector.GetInt32(7);
                                mecanico.SueldoTotal = lector.GetInt32(8);



                                // Mapea los datos del mecanico desde la base de datos al modelo.

                                return StatusCode(200, mecanico);
                            }
                            else
                            {
                                return StatusCode(404, "no se encontro nada ");
                            }




                        }
                    }

                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al procesar la solicitud.", Error = ex.Message });
            }
        }


        [HttpPost]
        // Agrega un nuevo mecanico


        public async Task<IActionResult> addMecanico([FromBody] MecanicoModelos mecanico)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {// Parámetros para el nuevo mecanico.
                    await conn.OpenAsync();
                    string query = "INSERT INTO mecanicos (idMecanico, Nombre, Edad, Domicilio, Titulo, Especialidad, SueldoBase, GrantTitulo, SueldoTotal) " +
                        "VALUES (@idMecanico, @Nombre, @Edad, @Domicilio, @Titulo, @Especialidad, @SueldoBase, @GrantTitulo, @SueldoTotal)";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {//vamos poniendo cada parametro 
                        command.Parameters.AddWithValue("@idMecanico", mecanico.idMecanico);
                        command.Parameters.AddWithValue("@Nombre", mecanico.Nombre);
                        command.Parameters.AddWithValue("@Edad", mecanico.Edad);
                        command.Parameters.AddWithValue("@Domicilio", mecanico.Domicilio);
                        command.Parameters.AddWithValue("@Titulo", mecanico.Titulo);
                        command.Parameters.AddWithValue("@Especialidad", mecanico.Especialidad);
                        command.Parameters.AddWithValue("@SueldoBase", mecanico.SueldoTotal);
                        command.Parameters.AddWithValue("@GrantTitulo", mecanico.GrantTitulo);
                        command.Parameters.AddWithValue("@SueldoTotal", mecanico.SueldoTotal);
                        await command.ExecuteNonQueryAsync();
                        return Ok(mecanico);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al procesar la solicitud.", Error = ex.Message });
            }
        }

        [HttpPut("{idMecanico}")]
        // Edita un mecanico existente

        public async Task<IActionResult> EditMecanico(int id, [FromBody] MecanicoModelos mecanico)
        {
            try
            {                    // Parámetros para actualizar el mecanico.

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    string query = @"UPDATE mecanicos 
                                 SET Nombre = @Nombre, 
                                  Edad = @Edad, 
                                 Domicilio = @Domicilio, 
                                 Titulo = @Titulo, 
                                 Especialidad = @Especialidad,
                                 SueldoBase =@SueldoBase,
                                 GrantTitulo = @GrantTitulo,
                                 SueldoTotal = @SueldoTotal
                                 WHERE idMecanico = @id";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@id", mecanico.idMecanico);
                        command.Parameters.AddWithValue("@Nombre", mecanico.Nombre);
                        command.Parameters.AddWithValue("@Edad", mecanico.Edad);
                        command.Parameters.AddWithValue("@Domicilio", mecanico.Domicilio);
                        command.Parameters.AddWithValue("@Titulo", mecanico.Titulo);
                        command.Parameters.AddWithValue("@Especialidad", mecanico.Especialidad);
                        command.Parameters.AddWithValue("@SueldoBase", mecanico.SueldoBase);
                        command.Parameters.AddWithValue("@GrantTitulo", mecanico.GrantTitulo);
                        command.Parameters.AddWithValue("@SueldoTotal", mecanico.SueldoTotal);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return StatusCode(200, mecanico);
                        }
                        else
                        {
                            return StatusCode(404, "mecanico no encontrado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al procesar la solicitud.", Error = ex.Message });
            }

        }



        [HttpDelete("{idMecanico}")]
        // Elimina un mecanico por ID
        public async Task<IActionResult> DeleteMecanico(int idMecanico)
        {
            try
            { //aqui hacenos todo el metodo donde seleccionamos la id del mecanico que queremos eliminar
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    string query = "DELETE FROM mecanicos WHERE idMecanico = @id";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.Add(new MySqlParameter("@id", idMecanico));

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                        {
                            return StatusCode(200, "Mecanico eliminado");
                        }
                        else
                        {
                            return StatusCode(404, "El mecanico no existe");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al procesar la solicitud.", Error = ex.Message });
            }


        }
    }
}