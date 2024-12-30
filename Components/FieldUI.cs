using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP.Components
{
    internal class FieldUI
    {
        public Button deleteBtn;
        public TextBox nameInput;
        public ComboBox typeComboBox;
        public TextBox dataInput;
        public ErrorProvider errorType;
        public ErrorProvider errorName;
        public bool isSchemaField;

        public FieldUI(Type fieldType, string fieldName = "", bool isSchemaField = false)
        {
            this.isSchemaField = isSchemaField;

            this.deleteBtn = new Button
            {
                Text = "X",
                BackColor = Color.FromArgb(192, 192, 192),
                ForeColor = Color.Black,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Width = 30,
                Height = 30,
                Anchor = AnchorStyles.Top,
            };

            this.nameInput = new TextBox
            {
                Width = 150,
                Text = fieldName,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
            };

            this.typeComboBox = new ComboBox
            {
                Width = 150,
                Anchor = AnchorStyles.Top,
            };

            this.errorName = new ErrorProvider();


            foreach (Type type in Constants.supportedType)
            {
                this.typeComboBox.Items.Add(type);
            }

            this.typeComboBox.SelectedIndex = Constants.supportedType.FindIndex(t => t == fieldType);

            if(!isSchemaField)
            {
                this.dataInput = new TextBox
                {
                    Width = 325,
                    Anchor = AnchorStyles.Left | AnchorStyles.Top,
                };
                this.errorType = new ErrorProvider();
            }
            else
            {
                this.dataInput = null;
                this.errorType = null;
            }
        }
        public bool validateDataInput()
        {
            Type selectedType = Constants.supportedType[this.typeComboBox.SelectedIndex];
            string data = this.dataInput.Text;

            // Clear any previous error
            this.errorType.SetError(this.dataInput, "");

            if (string.IsNullOrWhiteSpace(data))
            {
                this.errorType.SetError(this.dataInput, "Data field cannot be empty.");
                return false;
            }
            if (selectedType == null)
            {
                return false;
            }

            try
            {
                // Validate based on the selected type
                if (selectedType == typeof(int))
                {
                    int.Parse(data);
                }
                else if (selectedType == typeof(long))
                {
                    long.Parse(data);
                }
                else if (selectedType == typeof(decimal))
                {
                    decimal.Parse(data);
                }
                else if (selectedType == typeof(double))
                {
                    double.Parse(data);
                }
                else if (selectedType == typeof(string))
                {
                    // Strings are always valid, but you can add custom rules
                    return true;
                }
                else if (selectedType == typeof(bool))
                {
                    try
                    {
                        if (!Constants.booleanTypeValidation.Contains(data))
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception ex)
                    {
                        errorType.SetError(dataInput, $"Value must be 'true'/'false'");
                        return false;
                    }
                }
                else if (selectedType == typeof(DateTime))
                {
                    try
                    {
                        DateTime.Parse(data);
                    }
                    catch (Exception ex)
                    {
                        errorType.SetError(dataInput, $"Date format must be MM/DD/YYYY");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                errorType.SetError(dataInput, $"Invalid {typeComboBox.SelectedItem?.ToString()} value");
                return false;
            }
            errorType.SetError(dataInput, "");
            return true;
        }
    }
}
