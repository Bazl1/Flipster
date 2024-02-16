import { API_URL } from "../../src/shared/axios";

describe("Change avatar", () => {
    beforeEach(() => {
        cy.visit("/login");
        cy.intercept("POST", `${API_URL}/auth/login`).as("loginRequest");

        cy.get('[data-cy="input-email"]').type("test@gmail.com");
        cy.get('[data-cy="input-password"]').type("qwerty123");
        cy.get('[data-cy="submit"]').click();

        cy.wait("@loginRequest").then((interception) => {
            expect(interception.request.method).to.equal("POST");
            expect(interception.request.url).to.include(`${API_URL}/auth/login`);
            expect(interception.response?.statusCode.toString()).to.equal("200");
        });

        cy.visit("/profile");
    });

    it("Visit the profile page", () => {
        cy.url().should("include", "/profile");
    });

    it("Check if there is a settings tab", () => {
        cy.get('[data-cy="settings-btn"]').should("exist");
    });

    it("Whether the settings tab is turned on and has all the items there", () => {
        cy.get('[data-cy="settings-btn"]').click();

        cy.get("button").contains("Change avatar").should("exist");
        cy.get("button").contains("Change contact details").should("exist");
        cy.get("button").contains("Change the number").should("exist");
        cy.get("button").contains("Change password").should("exist");
    });

    // it("Name and location change check", () => {
    //     cy.get('[data-cy="settings-btn"]').click();
    //     cy.get("button").contains("Change contact details").click();

    //     cy.get('[data-cy="change-location"]').should("exist");
    //     cy.get('[data-cy="change-name"]').should("exist");
    //     cy.get('[data-cy="submit"]').should("exist");

    //     cy.get('[data-cy="change-location"]').type("Kyiv");
    // });
});
