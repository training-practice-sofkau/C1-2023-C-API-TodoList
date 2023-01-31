using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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


        public TodoListController(TodolistContext dbContext)
        {

            _dbContext = dbContext;

        }

        /// <summary>
        /// Funcional
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Medio funcional
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Consultar un solo registro
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<TodoItem>> GetItem([FromRoute] int id)
        {
            try
            {
                var item = await _dbContext.TodoItems.FindAsync(id);
               // var result = _dbContext.TodoItems.Where(r => r.Estate != 0).ToList();


                if (item.Estate == 0)
                {

                    return NotFound(new { 
                    
                    message = "La tarea con ese id no existe"
                    
                    });

                }

                return Ok(item);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        /// <summary>
        /// medio funcional
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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


        /// <summary>
        /// medio funcional
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoitemAc"></param>
        /// <returns></returns>

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> ActualizarItem([FromRoute] int id, TodoItemActualizar todoitemAc)
        {
            var item = await _dbContext.TodoItems.FindAsync(id);


            if (item != null){

                item.Title = todoitemAc.Title;
                item.Descripcion = todoitemAc.Descripcion;
                item.Responsible = todoitemAc.Responsible;
                item.IsCompleted = todoitemAc.IsCompleted;


                await _dbContext.SaveChangesAsync();

                
            }

            return Ok("La tarea se ha actualizado de forma correcta!");

        }


        /// <summary>
        /// medio funcional
        /// </summary>
        /// <param name="id"></param>
        /// <param name="complete"></param>
        /// <returns></returns>
            [HttpPut]
            [Route("/completed/{id:int}")]
            public async Task<IActionResult> ActualizarOneItem([FromRoute] int id, bool complete){

            //var result = _dbContext.TodoItems.Where(r => r.Estate == 0).ToList();

            try
            { 
                
                var respon = await _dbContext.TodoItems.FindAsync(id);
                respon.IsCompleted = complete;
                await _dbContext.SaveChangesAsync();

                return Ok("La tarea se ha editado de forma correcta!");


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


        /// <summary>
        /// Funcional
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

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
            catch (Exception) { 
                throw;
            }

                
            }


        private bool ItemAvailable(int id)
        {

            return (_dbContext.TodoItems?.Any(x => x.Id == id)).GetValueOrDefault();

        }

    }


}
