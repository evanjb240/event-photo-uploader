<template>
    <div>
        <h1 class="centered">
            Gift Registry
        </h1>
        <p class="centered">
            Your presence and time with us in Punta Cana is truly enough. However, if you still wish to send a gift, we are registered at Amazon, Target and Crate & Barrel. Please click the store below.  
        </p>
        <div class="centered">
            <a class="card" href="https://www.target.com/gift-registry/gift/karla-and-evan"><img src="@/public/images/TargetLogo.jpg" width="75" height="75"></a>
            <a class="card" href="https://www.amazon.com/wedding/share/karla-and-evan"><img src="@/public/images/AmazonLogo.webp" width="150" height="50"></a>
        </div>
        <p class="centered">
            If you prefer a gift of cash, we have set up an account with Joy to contribute to our bathroom remodel.
        </p>
    </div>
</template>
<script setup>
</script>
<style scoped>
.centered{
  display:flex;
  justify-content: center;
  text-align: center;
  gap:20px;
  align-items: center;

}
.card {
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;
  width: 20%;
  height: 100px;
  background-color: white;
  display: flex;
  align-items: center;
  justify-content: center;
}

h1 {
	font-family: 'Dancing Script', cursive;
	font-size: 80px;
  font-weight: 900;
	margin-bottom: 30px;
}

.card:hover {
  box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
}
p{
  font-size: 18px;
}
</style>