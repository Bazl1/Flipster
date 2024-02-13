import ChangeAvatarForm from "../ChangeAvatarForm/ChangeAvatarForm";
import ChangeContactDetailsFrom from "../ChangeContactDetailsFrom/ChangeContactDetailsFrom";
import ChangeNumberForm from "../ChangeNumberForm/ChangeNumberForm";
import ChangePasswordForm from "../ChangePasswordForm/ChangePasswordForm";
import FAQ from "../FAQ/FAQ";
import s from "./Settings.module.scss";

const Settings = () => {
    return (
        <div className={s.settings}>
            <FAQ title="Change avatar" children={<ChangeAvatarForm />} />
            <FAQ
                title="Change contact details"
                children={<ChangeContactDetailsFrom />}
            />
            <FAQ title="Change the number" children={<ChangeNumberForm />} />
            <FAQ title="Change password" children={<ChangePasswordForm />} />
        </div>
    );
};

export default Settings;
