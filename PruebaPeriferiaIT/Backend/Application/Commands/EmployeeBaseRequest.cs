﻿namespace PruebaPeriferiaIT.Application.Commands
{
    public class EmployeeBaseRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public int Position { get; set; }
    }
}
