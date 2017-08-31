using MassTransit;
using Me20.Contracts.Commands;
using Me20.Identity.Entities;
using Me20.Identity.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Identity.WriteService.CommandConsumers
{
    public class CreateNewUserCommandConsumer : IConsumer<ICreateNewUserCommand>
    {
        private readonly UserRepository repository;

        public CreateNewUserCommandConsumer(/*UserRepository repository*/)
        {
            this.repository = new UserRepository();//repository; 
        }

        public async Task Consume(ConsumeContext<ICreateNewUserCommand> context)
        {
            var message = context.Message;
            var userToCreate = new UserEntity(id: message.Id,
                authenticationType: message.AuthenticationType);

            await repository.AddAsync(userToCreate);
        }
    }
}
