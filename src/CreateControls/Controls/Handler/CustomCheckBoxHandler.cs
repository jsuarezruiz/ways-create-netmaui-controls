namespace CreateControls.Controls
{
    public partial class CustomCheckBoxHandler
    {
        public static IPropertyMapper<ICustomCheckBox, CustomCheckBoxHandler> Mapper = new PropertyMapper<ICustomCheckBox, CustomCheckBoxHandler>(ViewMapper)
        {
            [nameof(ICustomCheckBox.IsChecked)] = MapIsChecked,
        };

        public static CommandMapper<ICheckBox, CustomCheckBoxHandler> CommandMapper = new(ViewCommandMapper);

        public CustomCheckBoxHandler() : base(Mapper)
        {

        }

        public CustomCheckBoxHandler(IPropertyMapper mapper) : base(mapper ?? Mapper)
        {

        }
    }
}