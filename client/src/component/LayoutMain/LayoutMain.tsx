import Footer from "../Footer/Footer";

const LayoutMain = ({ children }: any) => {
    return (
        <div className="wrapper">
            <main className="main">{children}</main>
            <Footer />
        </div>
    );
};

export default LayoutMain;
