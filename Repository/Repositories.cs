using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Models;

namespace SocialNetwork.Repository
{
    public class Repositories : IDisposable
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        private GenericRepository<UserTaskModel> userTaskRepository;
        private GenericRepository<SolutionModel> solutionRepository;
        private GenericRepository<TagModel> tagRepository;
        private GenericRepository<CategoryModel> categoryRepository;
        private GenericRepository<LikeModel> likeRepository;
        private GenericRepository<CommentModel> commentRepository;
        private GenericRepository<UserProposedSolutionModel> userProposedSolutionRepository;

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

        public GenericRepository<SolutionModel> SolutionRepository
        {
            get
            {
                if (this.solutionRepository == null)
                {
                    this.solutionRepository = new GenericRepository<SolutionModel>(dbContext);
                }
                return solutionRepository;
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

        public GenericRepository<UserProposedSolutionModel> UserProposedSolutionRepository
        {
            get
            {
                if (this.userProposedSolutionRepository == null)
                {
                    this.userProposedSolutionRepository = new GenericRepository<UserProposedSolutionModel>(dbContext);
                }
                return userProposedSolutionRepository;
            }
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