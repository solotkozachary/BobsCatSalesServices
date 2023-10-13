using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using zs.bcs.BobsCatSalesServices.Application.Entity.Commands;
using zs.bcs.BobsCatSalesServices.Application.Enumerations;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Persistence.SalesAssociate;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Services;
using zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Commands;
using zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Queries;
using zs.bcs.BobsCatSalesServices.Domain.Entity.EntityIdentity;
using zs.bcs.BobsCatSalesServices.Events;
using zs.bcs.BobsCatSalesServices.Events.SalesAssociate;

namespace zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Handlers
{
    /// <summary>
    /// The handler responsible for creating a new sales associate.
    /// </summary>
    public class CreateSalesAssociateHandler : IRequestHandler<CreateSalesAssociateCommand, string>
    {
        private readonly ILogger<CreateSalesAssociateHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ISalesAssociatePersistenceCommands _commands;
        private readonly IPasswordSecurityService _passwordService;

        // Use open generics : services.AddTransient(typeof(IBobsCatSalesEventService<>), typeof(BobsCatSalesEventServiceImpl<>));
        private readonly IBobsCatSalesEventService<SalesAssociateCreated, Domain.Entity.SalesAssociate.SalesAssociate> _eventService;

        public CreateSalesAssociateHandler(
            ILogger<CreateSalesAssociateHandler> logger,
            IMediator mediator,
            ISalesAssociatePersistenceCommands commands,
            IPasswordSecurityService passwordService,
            IBobsCatSalesEventService<SalesAssociateCreated, Domain.Entity.SalesAssociate.SalesAssociate> eventService
            )
        {
            _logger = logger;
            _mediator = mediator;
            _commands = commands;
            _passwordService = passwordService;
            _eventService = eventService;
        }

        public async Task<string> Handle(CreateSalesAssociateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateSalesAssociateHandler");

            await VerifyEmailNotUsed(request.EmailAddress, cancellationToken);

            var entity = await BuildSalesAssociate(request, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogTrace("Canceled CreateSalesAssociateHandler");

                cancellationToken.ThrowIfCancellationRequested();
            }

            await _commands.CreateSalesAssociate(entity, cancellationToken);

            await _eventService.PublishBobsCatSalesEvent(new SalesAssociateCreated(entity), cancellationToken);

            _logger.LogInformation("New sales associate successfully created - SalesAssociateEntityKey:{SalesAssociateEntityKey}", entity.EntityKey);

            _logger.LogTrace("Exit CreateSalesAssociateHandler - SalesAssociateEntityKey:{SalesAssociateEntityKey}", entity.EntityKey);

            return entity.EntityKey;
        }

        private async Task VerifyEmailNotUsed(string email, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter VerifyEmailNotUsed");

            var existing = await _mediator.Send(new GetSalesAssociateByEmailQuery { EmailAddress = email }, cancellationToken);

            if (existing != null)
            {
                // DO NOT PUT EMAIL ADDRESS IN LOG MESSAGES
                var errMsg = "Sales associate creation failed.";

                _logger.LogError(errMsg);
                _logger.LogTrace("Exit VerifyEmailNotUsed");

                throw new Exceptions.ApplicationException(ApplicationConcern.Entity, ApplicationRules.MustNotExist, errMsg);
            }

            _logger.LogTrace("Exit VerifyEmailNotUsed");
        }

        private async Task<Domain.Entity.SalesAssociate.SalesAssociate> BuildSalesAssociate(CreateSalesAssociateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter BuildSalesAssociate");

            var entity = await _mediator.Send(new InitializeEntityCommand<Domain.Entity.SalesAssociate.SalesAssociate>()
                { EntityCreationSourceId = nameof(CreateSalesAssociateHandler) });

            var passwordData = await _passwordService.GetPasswordData(request.Password, cancellationToken);
            entity.PasswordHash = passwordData.Item1;
            entity.PasswordSalt = passwordData.Item2;
            entity.PasswordCreatedDate = DateTime.Now;
            entity.UsedPasswordHashes = new List<byte[]>() { passwordData.Item1 };

            entity.PersonalDesignation = await GetPersonalDesignation(request, entity.EntityKey, cancellationToken);

            entity.LastSuccessfulLogin = DateTime.Now;

            entity.PhoneNumbers = await GetPhoneNumbers(request, entity.EntityKey, cancellationToken);
            entity.EmailAddresses = await GetEmails(request, entity.EntityKey, cancellationToken);

            _logger.LogTrace("Exit BuildSalesAssociate - SalesAssociateEntityKey:{SalesAssociateEntityKey}", entity.EntityKey);

            return entity;
        }

        private async Task<PersonalDesignation> GetPersonalDesignation(CreateSalesAssociateCommand request, string salesAssociateEntityKey, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetPersonalDesignation - SalesAssociateEntityKey:{SalesAssociateEntityKey}", salesAssociateEntityKey);

            var personalDesignation = await _mediator.Send(new InitializeEntityCommand<PersonalDesignation>()
            { RelationalEntityKey = salesAssociateEntityKey, EntityCreationSourceId = salesAssociateEntityKey });

            personalDesignation.FirstName = request.FirstName;
            personalDesignation.LastName = request.LastName;
            personalDesignation.MiddleInitial = request.MiddleInitial;
            personalDesignation.Prefix = request.Prefix;
            personalDesignation.Suffix = request.Suffix;

            _logger.LogTrace("Exit GetPersonalDesignation - SalesAssociateEntityKey:{SalesAssociateEntityKey}", salesAssociateEntityKey);

            return personalDesignation;
        }

        private async Task<IEnumerable<Phone>> GetPhoneNumbers(CreateSalesAssociateCommand request, string salesAssociateEntityKey, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetPhoneNumbers - SalesAssociateEntityKey:{SalesAssociateEntityKey}", salesAssociateEntityKey);

            var result = new List<Phone>();

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                var phone = await _mediator.Send(new InitializeEntityCommand<Phone>()
                { RelationalEntityKey = salesAssociateEntityKey, EntityCreationSourceId = salesAssociateEntityKey });

                phone.CountryCode = request.PhoneCountryCode;
                phone.AreaCode = request.PhoneAreaCode;
                phone.PhoneNumber = request.PhoneNumber;
                phone.PhoneType = request.PhoneType;
                phone.IsPreferred = true;
                phone.HasSmsConsent = true;

                result.Add(phone);
            }

            _logger.LogTrace("Exit GetPhoneNumbers - SalesAssociateEntityKey:{SalesAssociateEntityKey}", salesAssociateEntityKey);

            return result;
        }

        private async Task<IEnumerable<Email>> GetEmails(CreateSalesAssociateCommand request, string salesAssociateEntityKey, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetEmails - SalesAssociateEntityKey:{SalesAssociateEntityKey}", salesAssociateEntityKey);

            var result = new List<Email>();

            var email = await _mediator.Send(new InitializeEntityCommand<Email>()
            { RelationalEntityKey = salesAssociateEntityKey, EntityCreationSourceId = salesAssociateEntityKey });

            email.EmailType = request.EmailType;
            email.EmailAddress = request.EmailAddress;
            email.IsPreferred = true;
            email.HasEmailConsent = true;

            result.Add(email);

            _logger.LogTrace("Exit GetEmails - SalesAssociateEntityKey:{SalesAssociateEntityKey}", salesAssociateEntityKey);

            return result;
        }
    }
}
