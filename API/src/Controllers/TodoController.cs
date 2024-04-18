using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoList.Interfaces;
using TodoList.Models;

namespace TodoList.Controllers;

[ApiController]
[Route("api")]
public class TodoController : ControllerBase
{
    [HttpGet]
    [Route("{id:guid:required}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id, [FromServices] ITodoRepository todoRepository, [FromServices] IMapper mapper)
    {
        var todo = await todoRepository.Get(id);

        if (todo is null)
        {
            return NotFound();
        }

        var result = mapper.Map<Todo>(todo);
        
        return Ok(result);
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll([FromServices] ITodoRepository todoRepository, [FromServices] IMapper mapper)
    {
        var todos = await todoRepository.GetAll();

        if (todos is null)
        {
            return NotFound();
        }

        var result = mapper.Map<List<Todo>>(todos);

        return Ok(result);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] Todo createTodo, [FromServices] ITodoRepository todoRepository, [FromServices] IMapper mapper)
    {
        var todo = mapper.Map<Todo>(createTodo);

        var result = await todoRepository.Create(todo);

        if (result is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut]
    [Route("{id:guid:required}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Todo updateTodo, [FromServices] ITodoRepository todoRepository, [FromServices] IMapper mapper)
    {
        var todo = mapper.Map<Todo>(updateTodo);

        var result = await todoRepository.Update(todo, id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete]
    [Route("{id:guid:required}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, [FromServices] ITodoRepository todoRepository)
    {
        var result = await todoRepository.Delete(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
