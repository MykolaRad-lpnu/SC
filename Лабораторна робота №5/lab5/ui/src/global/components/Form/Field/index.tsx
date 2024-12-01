import { ControllerRenderProps } from "react-hook-form";
import { FormConfigProps } from "../../../../types";
import { Input } from "./input";
import { Select } from "./select";
import { Textarea } from "./textarea";
import { Checkbox, CheckboxField } from "./checkbox";
import { Listbox, ListboxLabel, ListboxOption } from "./listbox";
import { ErrorMessage, Field, Label } from "./field";

type MyFieldProps = {
  field: ControllerRenderProps<
    {
      [x: string]: any;
    },
    string
  >;
  config: FormConfigProps<any>["fields"][""];
  error: string | undefined;
};

export const MyField = ({ field, config, error }: MyFieldProps) => {
  const {
    label,
    as,
    inputProps,
    selectProps,
    textareaProps,
    checkboxProps,
    listboxProps,
    useGetOptions,
  } = config;

  const options = useGetOptions?.();

  switch (as) {
    case "input":
      return (
        <Field>
          <Label>{label}</Label>
          <Input {...field} {...inputProps} />
          <ErrorMessage>{error}</ErrorMessage>
        </Field>
      );

    case "select":
      return (
        <Field>
          <Label>{label}</Label>
          <Select {...field} {...selectProps}>
            {(selectProps?.options || options)?.map(({ label, value }, index) => (
              <option key={index + "otion" + label} value={value}>
                {label}
              </option>
            ))}
          </Select>
          <ErrorMessage>{error}</ErrorMessage>
        </Field>
      );

    case "textarea":
      return (
        <Field>
          <Label>{label}</Label>
          <Textarea {...field} {...textareaProps} />
          <ErrorMessage>{error}</ErrorMessage>
        </Field>
      );

    case "checkbox":
      return (
        <CheckboxField>
          <Checkbox {...field} {...checkboxProps} />
          <Label>{label}</Label>
          <ErrorMessage>{error}</ErrorMessage>
        </CheckboxField>
      );

    case "listbox":
      return (
        <Field>
          <Label>{label}</Label>
          <Listbox {...field} {...listboxProps}>
            {(listboxProps?.options || options)?.map(({ value, label }, idx) => (
              <ListboxOption key={idx + "list" + label} value={value}>
                <ListboxLabel>{label}</ListboxLabel>
              </ListboxOption>
            ))}
          </Listbox>
          <ErrorMessage>{error}</ErrorMessage>
        </Field>
      );

    default:
      return null;
  }
};
