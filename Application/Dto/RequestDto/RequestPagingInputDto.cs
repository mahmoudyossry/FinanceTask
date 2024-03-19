
using Finance.Application.Dto;
using Finance.Core.IDto;
using System;
using System.Collections.Generic;

namespace Finance.Application.Services
{
    public class RequestPagingInputDto : PagingInputDto, IRequestPagingInputDto
    {
        public long? StatusId { get ; set ; }
        public string? Number { get ; set ; }
    }
}