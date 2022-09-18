﻿using System.Diagnostics.CodeAnalysis;

namespace CosmosCRUD.DTOs
{
    public class UserRequestDTO
    {
        [NotNull]
        public string FirstName { get; set; }

        [NotNull]
        public string MiddleName { get; set; }

        [NotNull]
        public string LastName { get; set; }

        [NotNull]
        public string PhoneNumber { get; set; }

        [NotNull]
        public string EmailAddress { get; set; }

    }
}
