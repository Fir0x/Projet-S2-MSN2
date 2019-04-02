from django.urls import path
from . import views
from django.views.generic import ListView, DetailView
from News.models import Article, Snippet


urlpatterns = [
    path('jeu', views.propos_projet ),
    path('',ListView.as_view(queryset=Article.objects.all().order_by("-date"),template_name="news/accueil.html")),
    path('MSN2', views.team),
    path('dwl',views.files),
    path('contact', views.snippet_detail),
             ]
