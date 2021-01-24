﻿using ParishManager.Data;
using ParishManager.Models.UserModels;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITimeSlotCommitmentService _timeSlotCommitmentService;

        public UserService(ApplicationDbContext context, ITimeSlotCommitmentService timeSlotCommitmentService)
        {
            _context = context;
            _timeSlotCommitmentService = timeSlotCommitmentService;
        }

        /* * * * * * * * * * * * * * * * * * * * * *
         * Anything dealing with modifying the     *
         * user is going to need the UserManager   *
         * but I don't want to deal with that      *
         * right now. This service will eventually *
         * serve as a facade for that manager      *
         * * * * * * * * * * * * * * * * * * * * * */

        public User CreateUser(UserCreate createModel)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAllUsers()
        {
            return _context.Users
                .ToList();
        }

        public ICollection<User> GetAllUsersForTimeSlot(int timeSlotId)
        {
            return _timeSlotCommitmentService
                .GetActiveTimeSlotCommitmentsForTimeSlot(timeSlotId)
                .Select(x => x.User)
                .ToList();
        }

        // have to finish after UserParishAssociationService
        public ICollection<User> GetAssociatedUsersForParish(int parishId)
        {
            throw new NotImplementedException();
        }

        // have to finish after UserParishAssociationService
        public ICollection<User> GetNonParishioners(int parishId)
        {
            throw new NotImplementedException();
        }

        // have to finish after UserParishAssociationService
        public ICollection<User> GetParishioners(int parishId)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userId)
        {
            return _context.Users
                .SingleOrDefault(x => x.Id == userId);
        }

        public User UpdateUser(UserUpdate updateModel)
        {
            throw new NotImplementedException();
        }
    }
}
