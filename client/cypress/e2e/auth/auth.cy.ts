describe("Create account", () => {
    it("visits the registration page", () => {
        cy.visit("/registration");
    });

    it("Checking for registration fields", () => {
        cy.get("[data-cy='input-name']").should("have.value", "");
        cy.get("[data-cy='input-email']").should("have.value", "");
        cy.get("[data-cy='input-password']").should("have.value", "");
        cy.get("[data-cy='input-confirm-password']").should("have.value", "");
    });

    it("Entering test data into the form", () => {
        cy.get("[data-cy='input-name']").type("Test Name");
        cy.get("[data-cy='input-email']").type("test@gmail.com");
        cy.get("[data-cy='input-password']").type("qwerty123");
        cy.get("[data-cy='input-confirm-password']").type("qwerty123");
    });
    it("Checking for a mandatory password match", () => {
        cy.get("[data-cy='input-confirm-password']").type("qwerty124");
        cy.get("[data-cy='submit']").click();

        cy.contains("A user with this e-mail address already exists").should(
            "exist",
        );
    });
});
