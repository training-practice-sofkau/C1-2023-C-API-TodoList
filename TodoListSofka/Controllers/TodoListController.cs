using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using TodoListSofka.Dto;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {

        private readonly TodolistContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public TodoListController(TodolistContext dbContext)
        {

            _dbContext = dbContext;

        }

        //Metodo listar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetItems()
        {
            try
            {
                var activeRecords = _dbContext.TodoItems.Where(r => r.Estate != 0).ToList();
                return activeRecords;
            }
            catch (Exception)
            {

                throw;
            }


        }

        //Consultar un solo registro
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<TodoItem>> GetItem([FromRoute] int id)
        {

            var item = await _dbContext.TodoItems.FindAsync(id);
            // var activeRecords = _dbContext.Programmers.Where(r => r.IsActive != 0).ToList();

            if (_dbContext == null)
            {

                return NotFound();

            }

            if (item == null || item.Estate == 0)
            {

                return BadRequest(new
                {

                    code = 400,
                    message = "No existe usuario con ese id por favor ingresar Id valido"

                });

            }

            return Ok(item);
        }


        //Metodo Post
        [HttpPost]
        public async Task<ActionResult> PostItem(TodoItemAgregar item)
        {
            var items = new TodoItem()
            {
                Title = item.Title,
                Descripcion = item.Descripcion,
                Responsible = item.Responsible,
                IsCompleted = item.IsCompleted,
                Estate = 1
            };


            await _dbContext.TodoItems.AddAsync(items);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        //Metodo editar
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> ActualizarItem([FromRoute] int id, TodoItemActualizar todoitemAc)
        {

            var result = _dbContext.TodoItems.Where(r => r.Estate == 0).ToList();

            for (int i = 0; i < result.Count; i++)
            {

                if (result[i].Estate == 0)
                {

                    return BadRequest(new
                    {

                        message = "Usuario a editar  no existe"

                    });

                }

            }

                var item = await _dbContext.TodoItems.FindAsync(id);

                if (item != null)
                {

                    item.Title = todoitemAc.Title;
                    item.Descripcion = todoitemAc.Descripcion;
                    item.Responsible = todoitemAc.Responsible;
                    item.IsCompleted = todoitemAc.IsCompleted;


                    await _dbContext.SaveChangesAsync();

                    return Ok("La tarea se ha actualizado de forma correcta!");
                }

                return NotFound(new {
                    code=404, 
                    message="Este item no se encuentra rgistrado en su lista de tareas" 
                    }
                );
            }


            [HttpPut]
            [Route("/completed/{id:int}")]
            public async Task<IActionResult> ActualizarOneItem([FromRoute] int id, bool complete){

              try{
                 var result = _dbContext.TodoItems.Where(r => r.Estate == 0).ToList();
                 for (int i = 0; i < result.Count; i++) {

                    if (result[i].Estate == 0) {

                        return BadRequest(new {
                            code = 403,
                            message = "Usuario a editar  no existe" });

                    }
                 }

                var respon = await _dbContext.TodoItems.FindAsync(id);
                respon.IsCompleted = complete;
                await _dbContext.SaveChangesAsync();

                return Ok("La tarea se ha eliminado de forma correcta!");


            }

            catch (DbUpdateConcurrencyException) {

                if (!ItemAvailable(id)) { 

                    return NotFound("Problemas en la actualizacion de la base de datos");

                }
                else{
                    throw;
                }

            }

            }


            //Metodo eliminar

            [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteItem(int id)
            {

                var item = await _dbContext.TodoItems.FindAsync(id);
                var recordToUpdate = _dbContext.TodoItems.FirstOrDefault(r => r.Id == id);

            try
            {
                if (recordToUpdate != null)
                {
                    recordToUpdate.Estate = 0;
                    _dbContext.SaveChanges();
                }
                else { return NotFound(); }


                return Ok(new
                {
                    code = 200,
                    message = $"El usuario con id {id} fue eliminado"
                     }
                );
            }
            catch (Exception)
            {

                throw;
            }

                
            }


        private bool ItemAvailable(int id)
        {

            return (_dbContext.TodoItems?.Any(x => x.Id == id)).GetValueOrDefault();

        }

    }


}






/* try
            {

                if (string.IsNullOrWhiteSpace(pro.CompleteName))
                {


                    return BadRequest(new
                    {
                        code = 400,
                        message = "El nombre es un dato requerido no dejar en blanco por favor"
                    });


                }


                if (string.IsNullOrWhiteSpace(programmer.Email))
                {


                    return BadRequest(new
                    {
                        code = 400,
                        message = "El correo electronico es un dato requerido no dejar en blanco por favor"
                    });

                }





                if (programmer.IsActive == 0)
                {


                    return BadRequest(new
                    {


                        message = "Usuario no existe"


                    });

                }

                if ((programmer.PhoneNumber.GetType()) == aux.GetType())
                {

                    return BadRequest(new
                    {


                        message = "No puedes ingresar texto en campos numericos "
                    });



                }

                await _dbContext.SaveChangesAsync();

            }

            catch (DbUpdateConcurrencyException)
            {

                    if (!ProgrammerAvailable(id))
                    {
                        return NotFound("Not fount line 128");
                    }
                    else {

                        throw;
                    
                    }

            }
*/