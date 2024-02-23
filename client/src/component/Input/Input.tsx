import s from "./Input.module.scss";

interface InputProps {
    text: string;
    registerName: string;
    type: string;
    value: any;
    setValue: (value: any) => void;
    register: any;
    errors: any;
    validationOptions?: any;
    dataСy?: string;
    placeholder?: string;
}

const Input: React.FC<InputProps> = ({
    text,
    registerName,
    type,
    value,
    setValue,
    register,
    errors,
    validationOptions,
    dataСy = null,
    placeholder = "",
}) => {
    return (
        <label className={s.input__columns}>
            {text && <span className={s.input__text}>{text}</span>}
            <input
                {...register(registerName, validationOptions)}
                value={value}
                onChange={(e) => {
                    setValue(e.target.value);
                }}
                className={s.input}
                type={type}
                data-cy={dataСy}
                placeholder={placeholder.toString()}
            />
            {errors[registerName] && <p className={s.input__error}>{errors[registerName]?.message}</p>}
        </label>
    );
};

export default Input;
