using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SurveyApp.Domain.Common;
using SurveyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories
{
    public class MongoDbRepository<T> : IRepository<T> where T : IMongoDbEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoDbRepository(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            var collectionName = typeof(T).Name + "s";
            _collection = database.GetCollection<T>(collectionName);
        }

        public void Add(T document)
        {
            _collection
                       .InsertOne(document);
        }

        public async Task AddAsync(T document)
        {
            await _collection
                             .InsertOneAsync(document);
        }

        public void DeleteById(string id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", id);
            _collection
                        .DeleteOne(filter);
        }

        public async Task DeleteByIdAsync(string id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", id);
            await _collection
                             .DeleteOneAsync(filter);
        }

        public IEnumerable<T?> GetAll()
        {
            return _collection
                              .Find(new BsonDocument())
                              .ToList();
        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            return await _collection
                                    .Find(new BsonDocument())
                                    .ToListAsync();
        }

        public IEnumerable<T?> GetAllWithPredicate(Expression<Func<T, bool>> predicate)
        {
            return _collection
                              .Find(predicate)
                              .ToList();
        }

        public async Task<IEnumerable<T?>> GetAllWithPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection
                                    .Find(predicate)
                                    .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _collection
                                    .Find(Builders<T>.Filter.Eq("Id", id))
                                    .SingleOrDefaultAsync();
        }

        public T? GetById(string id)
        {
            return _collection
                              .Find(Builders<T>.Filter.Eq("Id", id))
                              .SingleOrDefault();
        }

        public bool IsExists(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var document = _collection
                                      .Find(filter)
                                      .Any();
            return document;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var document = await _collection
                                            .Find(filter)
                                            .AnyAsync();
            return document;
        }

        public void Update(string id, T document)
        {
            _collection
                       .ReplaceOne(id, document);
        }

        public async Task UpdateAsync(string id, T document)
        {
            await _collection
                             .ReplaceOneAsync(id, document);
        }

        public void UpdateField<TField>(string id, string fieldName, TField value)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var update = Builders<T>.Update.Set(fieldName, value);
            _collection
                       .UpdateOne(filter, update);
        }

        public async Task UpdateFieldAsync<TField>(string id, string fieldName, TField value)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var update = Builders<T>.Update.Set(fieldName, value);
            await _collection
                             .UpdateOneAsync(filter, update);
        }
    }
}
