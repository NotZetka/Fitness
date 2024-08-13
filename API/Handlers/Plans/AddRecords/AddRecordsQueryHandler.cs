﻿using API.Data;
using API.Data.Repositories;
using API.Services;
using MediatR;

namespace API.Handlers.Plans.AddRecord
{
    public class AddRecordsQueryHandler : IRequestHandler<AddRecordsQuery, AddRecordsQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public AddRecordsQueryHandler(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        public async Task<AddRecordsQueryResult> Handle(AddRecordsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync(includeRecords: true);

            var exercises = user.FitnessPlans.SelectMany(x => x.Exercises).ToList();

            foreach (var exercise in exercises)
            {
                var record = request.Records.FirstOrDefault(x => x.ExerciseId == exercise.Id);
                if (record != null) exercise.Records.Add(new Record { 
                    Date = DateTime.Today,
                    Weight = record.Weight,
                    Repetitions = record.Repetitions
                });
            }

            await _unitOfWork.SaveChangesAsync();

            return new AddRecordsQueryResult();
        }
    }
}
