using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finance.Application.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [MaxLength(250, ErrorMessage = "Name cannot be more than 250 charachters")]
        public string? FirstName { get; set; }
        [MaxLength(250, ErrorMessage = "Name cannot be more than 250 charachters")]
        public string? LastName { get; set; }
        public string? ImgProfilePath { get; set; }
        public string Password { get; set; }

    }
}
