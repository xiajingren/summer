﻿using MediatR;
using Summer.Application.UnitOfWork;

namespace Summer.Application.Requests.Commands
{
    [UnitOfWork]
    public class UpdateCurrentUserPasswordCommand : IRequest
    {
        public string Password { get; set; }

        public string NewPassword { get; set; }

        public UpdateCurrentUserPasswordCommand(string password, string newPassword)
        {
            Password = password;
            NewPassword = newPassword;
        }
    }
}