using SEP.CustomClassBuilder;

namespace SEP.Interfaces
{
    internal interface IDatabase
    {
        Task<List<dbCollection>> GetAllCollections();
        Task<dbResponse> CreateNewCollection(string collectionName);
        Task<dbCollection> GetCollection(string collectionName);
        Task<dbDocument> GetDocumentByID(string collectionName, string id);
        Task<dbResponse> AddNewDocument(string collectionName, CustomClass newDocumentObject);
        Task<dbSchema> GetCollectionSchema(string collectionName);
        Task<dbResponse> UpdateDocumentByID(string collectionName, string id, CustomClass newDocumentObject);
        Task<dbResponse> DeleteDocumentByID(string collectionName, string id);
    }
    public class dbResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public dbResponse(bool isSuccess, string message) 
        {
            this.isSuccess = isSuccess;
            this.message = message;
        }
        public dbResponse() { }
    }
    public class dbCollection
    {
        public string collectionName { get; set; }
        public dbCollection(string collectionName)
        {
            this.collectionName = collectionName;
        }
        public dbCollection() { }
    }
    public class dbDocumentField
    {
        public string fieldName { get; set; }
        public string value { get; set; }
        public Type type { get; set; }
        public dbDocumentField(string fieldName, string value, Type type)
        {
            this.fieldName = fieldName;
            this.value = value;
            this.type = type;
        }
        public dbDocumentField() { }
    }
    public class dbDocument
    {
        public List<dbDocumentField> fields { get; set; }
        public dbDocument(List<dbDocumentField> fields) 
        {  
            this.fields = fields;
        }
        public dbDocument() { }
    }
    public class dbSchemaField
    {
        public string name { get; set; }
        public Type type { get; set; }
        public dbSchemaField(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }
        public dbSchemaField() { }

    }
    public class dbSchema
    {
        public string collectionName { get; set; }
        public List<dbSchemaField> fields { get; set; }
        public dbSchema(string collectionName, List<dbSchemaField> fields) 
        { 
            this.collectionName = collectionName;
            this.fields = fields; 
        }
        public dbSchema() { }
    }
}
