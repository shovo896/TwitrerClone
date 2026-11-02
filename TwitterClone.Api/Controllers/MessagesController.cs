using Microsoft.AspNetCore.Mvc;
using TwitterClone.Api.Models;
using TwitterClone.Api.Services;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MessagesController(SocialStore store) : ControllerBase
{
    [HttpPost]
    public ActionResult<MessageResponse> SendMessage(SendMessageRequest request)
    {
        if (store.GetUser(request.RecipientId) is null) return NotFound("Recipient was not found.");
        var message = store.AddMessage(SocialStore.DemoUserId, request);
        return CreatedAtAction(nameof(GetMessageById), new { id = message.Id }, message);
    }
    [HttpGet]
    public IActionResult GetMessages() => Ok(store.GetMessages(SocialStore.DemoUserId));
    [HttpGet("{id:guid}")]
    public ActionResult<MessageResponse> GetMessageById(Guid id) => store.GetMessage(id) is { } message ? Ok(message) : NotFound();
    [HttpPost("conversation/{conversationId:guid}")]
    public ActionResult<MessageResponse> SendMessageToConversation(Guid conversationId, SendMessageRequest request) => SendMessage(request);
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteMessage(Guid id) => store.DeleteMessage(id) ? NoContent() : NotFound();
    [HttpDelete("delete-all")]
    public IActionResult DeleteMessages() { store.DeleteMessages(SocialStore.DemoUserId); return NoContent(); }
    [HttpPatch("{id:guid}/mark-as-read")]
    public IActionResult MarkMessageAsRead(Guid id) => store.MarkMessageRead(id) ? Ok(store.GetMessage(id)) : NotFound();
}
