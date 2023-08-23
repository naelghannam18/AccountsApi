﻿using System.Net;
using AutoMapper;
using Application.DTOs;
using Domain.Exceptionsl;
using Domain.Models;
using Domain.Contracts.Infrastructure;
using MediatR;

namespace Application.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, Response<TransactionDTO>>
{
    #region Private Readonly Fields
    private readonly ITransactionsRepository TransactionsRepository;
    private readonly IMapper Mapper;
    #endregion

    #region Constructor
    public GetTransactionByIdQueryHandler(ITransactionsRepository transactionRepository, IMapper mapper)
    {
        TransactionsRepository = transactionRepository;
        Mapper = mapper;
    }
    #endregion

    #region Command Handler
    public async Task<Response<TransactionDTO>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = Mapper.Map<TransactionDTO>(await TransactionsRepository.GetById(request.transactionId));

        if (result is not null)
        {
            return new()
            {
                Status = HttpStatusCode.OK,
                Data = result
            };
        }
        else throw new TransactionNotFoundException(request.transactionId);
    } 
    #endregion
}
