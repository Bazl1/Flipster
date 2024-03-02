import { RefObject, useEffect } from "react";

interface useOutsideProps {
    ref: RefObject<HTMLElement>;
    setValue: (value: boolean) => void;
    value: boolean;
}

const useOutside = ({ ref, setValue, value }: useOutsideProps) => {
    const handleClickOutside = (event: any) => {
        if (ref.current && !ref.current.contains(event.target)) {
            setValue(false);
        }
    };

    useEffect(() => {
        if (value) {
            document.addEventListener("mousedown", handleClickOutside);
        } else {
            document.removeEventListener("mousedown", handleClickOutside);
        }
        return () => {
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, [value]);

    return null;
};

export default useOutside;
