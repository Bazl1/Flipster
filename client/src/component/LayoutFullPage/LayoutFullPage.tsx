const LayoutFullPage = ({ children }: any) => {
    return (
        <div className="wrapper">
            <main className="main">{children}</main>
        </div>
    );
};

export default LayoutFullPage;
