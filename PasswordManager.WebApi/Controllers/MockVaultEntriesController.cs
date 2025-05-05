using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Web;

namespace PasswordManager.WebApi.Controllers
{
    using TModel = PasswordManager.WebApi.Models.MockVaultEntry;
    using TEntity = PasswordManager.Logic.Entities.MockVaultEntry;
    using TContract = PasswordManager.Common.Contracts.IMockVaultEntry;

    [Route("api/[controller]")]
    [ApiController]
    public sealed class MockVaultEntriesController : ControllerBase
    {
        /// <summary>
        /// Gets the context for the database operations.
        /// </summary>
        /// <returns>The database context.</returns>
        protected Logic.Contracts.IContext GetContext()
        {
            return Logic.DataContext.Factory.CreateContext();
        }

        /// <summary>
        /// Gets the DbSet for the MockVaultEntry entity.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <returns>The DbSet for the company entity.</returns>
        protected DbSet<TEntity> GetDbSet(Logic.Contracts.IContext context)
        {
            return context.MockVaultEntriesSet;
        }

        protected TModel ToModel(TEntity entity)
        {
            var handled = false;
            var result = new TModel();
            BeforeToModel(entity, ref result, ref handled);
            if (handled == false)
            {
                (result as TContract).CopyProperties(entity);
            }
            AfterToModel(result);
            return result;
        }
        private void BeforeToModel(TEntity entity, ref TModel outModel, ref bool handled) { }
        private void AfterToModel(TModel model) { }

        /// <summary>
        /// Converts a MockVaultEntry model to a MockVaultEntry entity.
        /// </summary>
        /// <param name="model">The MockVaultEntry model.</param>
        /// <param name="entity">The existing MockVaultEntry entity, or null to create a new entity.</param>
        /// <returns>The MockVaultEntry entity.</returns>
        protected TEntity ToEntity(TModel model, TEntity? entity)
        {
            var handled = false;
            var result = entity ?? new TEntity();
            BeforeToEntity(model, ref result, ref handled);
            if (handled == false)
            {
                (result as TContract).CopyProperties(model);
            }
            AfterToEntity(result);
            return result;
        }
        private void BeforeToEntity(TModel model, ref TEntity outEntity, ref bool handled) { }
        private void AfterToEntity(TEntity entity) { }


        /// <summary>
        /// Gets a list of MockVaultEntries.
        /// </summary>
        /// <returns>A list of MockVaultEntries models.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TModel>> Get()
        {
            using var context = GetContext();
            var dbSet = GetDbSet(context);
            var querySet = dbSet.AsQueryable().AsNoTracking();
            var query = querySet.ToArray();
            var result = query.Select(e => ToModel(e));

            return Ok(result);
        }

        /// <summary>
        /// Queries MockVaultEntry based on a predicate.
        /// </summary>
        /// <param name="predicate">The query predicate.</param>
        /// <returns>A list of MockVaultEntries that match the predicate.</returns>
        [HttpGet("/api/[controller]/query/{predicate}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TModel>> Query(string predicate)
        {
            using var context = GetContext();
            var dbSet = GetDbSet(context);
            var querySet = dbSet.AsQueryable().AsNoTracking();
            var query = querySet.Where(HttpUtility.UrlDecode(predicate)).ToArray();
            var result = query.Select(e => ToModel(e));

            return Ok(result);
        }

        /// <summary>
        /// Gets a MockVaultEntry by its ID.
        /// </summary>
        /// <param name="id">The MockVaultEntry Guid.</param>
        /// <returns>The MockVaultEntry model.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TModel?> Get(Guid id)
        {
            using var context = GetContext();
            var dbSet = GetDbSet(context);
            var result = dbSet.FirstOrDefault(e => e.Guid == id);

            return result == null ? NotFound() : Ok(ToModel(result));
        }


        /// <summary>
        /// Updates an existing company.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <param name="model">The company model.</param>
        /// <returns>The updated company model.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TModel> Put(Guid id, [FromBody] TModel model)
        {
            try
            {
                using var context = GetContext();
                var dbSet = GetDbSet(context);
                var entity = dbSet.FirstOrDefault(e => e.Guid == id);

                if (entity != null)
                {
                    entity = ToEntity(model, entity);
                    context.SaveChanges();
                }
                return entity == null ? NotFound() : Ok(ToModel(entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Partially updates an existing company.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <param name="patchModel">The JSON patch document.</param>
        /// <returns>The updated company model.</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TModel> Patch(Guid id, [FromBody] JsonPatchDocument<TModel> patchModel)
        {
            try
            {
                using var context = GetContext();
                var dbSet = GetDbSet(context);
                var entity = dbSet.FirstOrDefault(e => e.Guid == id);

                if (entity != null)
                {
                    var model = ToModel(entity);

                    patchModel.ApplyTo(model);

                    (entity as TContract).CopyProperties(model);
                    context.SaveChanges();
                }
                return entity == null ? NotFound() : Ok(ToModel(entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a company by its ID.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <returns>No content if the deletion was successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(Guid id)
        {
            try
            {
                using var context = GetContext();
                var dbSet = GetDbSet(context);
                var entity = dbSet.FirstOrDefault(e => e.Guid == id);

                if (entity != null)
                {
                    dbSet.Remove(entity);
                    context.SaveChanges();
                }
                return entity == null ? NotFound() : NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Erstellt einen neuen MockVaultEntry.
        /// </summary>
        /// <param name="model">Das Model für den neuen Eintrag.</param>
        /// <returns>Der erstellte Eintrag mit Status 201 Created.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TModel> Post([FromBody] TModel model)
        {
            try
            {
                using var context = GetContext();
                var dbSet = GetDbSet(context);

                var entity = ToEntity(model, null);
                dbSet.Add(entity);
                context.SaveChanges();

                var resultModel = ToModel(entity);
                return CreatedAtAction(nameof(Get), new { id = entity.Guid }, resultModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}