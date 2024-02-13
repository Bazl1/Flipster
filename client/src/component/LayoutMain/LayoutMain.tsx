import Footer from "../Footer/Footer";
import Header from "../Header/Header";

const LayoutMain = ({ children }: any) => {
    return (
        <div className="wrapper">
            <Header />
            <main className="main">{children}</main>
            <Footer />
        </div>
    );
};

export default LayoutMain;
