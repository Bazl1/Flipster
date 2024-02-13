import s from "./Textarea.module.scss";

interface InputProps {
    text: string;
    registerName: string;
    value: string;
    setValue: (value: string) => void;
    register: any;
    errors: any;
    validationOptions?: any;
    dataСy?: string;
}

const Textarea: React.FC<InputProps> = ({
    text,
    registerName,
    value,
    setValue,
    register,
    errors,
    validationOptions,
    dataСy = null,
}) => {
    return (
        <label className={s.input__columns}>
            <span className={s.input__text}>{text}</span>
            <textarea
                {...register(registerName, validationOptions)}
                value={value}
                onChange={(e) => {
                    setValue(e.target.value);
                }}
                className={s.input}
                data-cy={dataСy}
                rows={10}
            ></textarea>
            {errors[registerName] && (
                <p className={s.input__error}>
                    {errors[registerName]?.message}
                </p>
            )}
        </label>
    );
};

export default Textarea;
