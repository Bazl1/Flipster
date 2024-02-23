import Footer from "../Footer/Footer";
import Header from "../Header/Header";
import { Toaster } from "react-hot-toast";

const LayoutMain = ({ children }: any) => {
    return (
        <div className="wrapper">
            <Header />
            <main className="main">{children}</main>
            <Footer />
            <Toaster position="bottom-left" reverseOrder={false} />
        </div>
    );
};

export default LayoutMain;
