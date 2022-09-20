using ClassLibraryAPI;

namespace MauiAppAPI.Services
{
    public interface IRestService
    {
        List<CommentDTO> Comments { get; }
        Task<List<CommentDTO>> RefreshDataAsync();
        Task SendCommentAsync(CommentDTO item);
    }
}