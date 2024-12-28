using SEP.CustomClassBuilder;

namespace SEP.Interfaces
{
    internal interface IDatabase
    {
        Task<List<dbCollection>> GetAllCollections();
        Task<dbResponse> CreateNewCollection(string collectionName);
        Task<List<dbDocument>> GetCollection(string collectionName);
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
        public dbDocumentField getFieldByName(string fieldName)
        {
            return fields.Find(field =>  field.fieldName == fieldName);
        }
        public List<(string PropertyName, Type PropertyType, string PropertyValue)> toDocumentList()
        {
            return fields?.Select(field => (field.fieldName, field.type, field.value)).ToList()
                ?? new List<(string, Type, string)>();
        }
        public List<(string PropertyName, Type PropertyType, string PropertyValue)> toDocumentListWithoutID()
        {
            List<dbDocumentField> fieldsWithNoID = this.fields.Where(x => x.fieldName != "_id").ToList();
            return fieldsWithNoID?.Select(field => (field.fieldName, field.type, field.value)).ToList()
                ?? new List<(string, Type, string)>();
        }
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
        public List<(string PropertyName, Type PropertyType)> toSchemaList()
        {
            List<dbSchemaField> fieldsWithNoID = this.fields.Where(x => x.name != "_id").ToList();
            return fieldsWithNoID?.Select(field => (field.name, field.type)).ToList() 
                ?? new List<(string, Type)>();
        }
    }
}
