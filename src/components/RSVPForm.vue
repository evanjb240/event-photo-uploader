<template>
    <div class="container">
        <h2>RSVP</h2>
        <form action="/action_page.php">
            <div>
                <div>
                    <label for="fname">Please respond by xxxx:</label>
                </div>
                <input type="radio" id="rsvp_accept" name="rsvp_choice" value="Accept">
                <label for="rsvp_accept">Gladly Accepts</label>
                <input type="radio" id="rsvp_decline" name="rsvp_choice" value="Deline">
                <label for="rsvp_decline">Regretfully Declines</label>
            </div>
            <div>
                <div>
                    <label for="fname">Please add a food choice:</label>
                </div>
                <input type="radio" id="rsvp_food_1" name="rsvp_food_choice" value="Salmon">
                <label for="rsvp_food_1">Salmon</label>
                <input type="radio" id="rsvp_food_2" name="rsvp_food_choice" value="Chicken">
                <label for="rsvp_food_2">Chicken</label>
                <input type="radio" id="rsvp_food_3" name="rsvp_food_choice" value="Vegetarian">
                <label for="rsvp_food_3">Vegetarian</label>
            </div>
            <input type="submit" value="Submit">
        </form>
    </div>
</template>