using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using SocialNetwork.Models;

namespace SocialNetwork.Repository
{
    public class Repositories : IDisposable
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        private GenericRepository<UserTaskModel> userTaskRepository;
        private GenericRepository<TaskSolutionModel> taskSolutionRepository;
        private GenericRepository<TagModel> tagRepository;
        private GenericRepository<CategoryModel> categoryRepository;
        private GenericRepository<LikeModel> likeRepository;
        private GenericRepository<CommentModel> commentRepository;
        private GenericRepository<UserSolvedTaskModel> userSolvedTaskRepository;

        public GenericRepository<UserTaskModel> UserTaskRepository
        {
            get
            {
                if (this.userTaskRepository == null)
                {
                    this.userTaskRepository = new GenericRepository<UserTaskModel>(dbContext);
                }
                return userTaskRepository;
            }
        }

        public GenericRepository<TaskSolutionModel> TaskSolutionRepository
        {
            get
            {
                if (this.taskSolutionRepository == null)
                {
                    this.taskSolutionRepository = new GenericRepository<TaskSolutionModel>(dbContext);
                }
                return taskSolutionRepository;
            }
        }

        public GenericRepository<TagModel> TagRepository
        {
            get
            {
                if (this.tagRepository == null)
                {
                    this.tagRepository = new GenericRepository<TagModel>(dbContext);
                }
                return tagRepository;
            }
        }

        public GenericRepository<CategoryModel> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<CategoryModel>(dbContext);
                }
                return categoryRepository;
            }
        }

        public GenericRepository<LikeModel> LikeRepository
        {
            get
            {
                if (this.likeRepository == null)
                {
                    this.likeRepository = new GenericRepository<LikeModel>(dbContext);
                }
                return likeRepository;
            }
        }

        public GenericRepository<CommentModel> CommentRepository
        {
            get
            {
                if (this.commentRepository == null)
                {
                    this.commentRepository = new GenericRepository<CommentModel>(dbContext);
                }
                return commentRepository;
            }
        }

        public GenericRepository<UserSolvedTaskModel> UserSolvedTaskRepository
        {
            get
            {
                if (this.userSolvedTaskRepository == null)
                {
                    this.userSolvedTaskRepository = new GenericRepository<UserSolvedTaskModel>(dbContext);
                }
                return userSolvedTaskRepository;
            }
        }

        public Collection<UserTaskModel> GetUserSolvedTasks(string id)
        {
            var userSolvedTasks = userSolvedTaskRepository.Get().Where(x => x.UserId == id);
            Collection<UserTaskModel> solvedTasks = new Collection<UserTaskModel>();
            foreach (var userSolvedTask in userSolvedTasks)
            {
                solvedTasks.Add(userTaskRepository.GetById(userSolvedTask.Id));
            }
            return solvedTasks;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}