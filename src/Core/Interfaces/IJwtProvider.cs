using Bainah.Core.Entities;
namespace Bainah.Core.Interfaces;
public interface IJwtProvider { string GenerateToken(User user, IEnumerable<string> roles); }
