import React from "react";
import { NavLink } from "react-router-dom";
import { Button, Container, Menu } from "semantic-ui-react";

export default function NavBar() {
   return (
      <Menu inverted fixed="top">
         <Container>
            <Menu.Item as={NavLink} to="/" exact header>
               <img
                  src="/assets/logo.png"
                  alt="logo"
                  style={{ marginRight: "15px" }}
               />
               Reactivities
            </Menu.Item>
            <Menu.Item name="Activities" as={NavLink} to="/activities" />
            <Menu.Item name="Errors" as={NavLink} to="/errors" />
            <Menu.Item>
               <Button
                  as={NavLink}
                  to="/createActivity"
                  positive
                  content="Create Activity"
               />
            </Menu.Item>
         </Container>
      </Menu>
   );
}
